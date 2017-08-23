using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.DataAccess.Infrastructure;
using GameStore.DataAccess.Interfaces;
using GameStore.DataAccess.MSSQL.Entities;
using GameStore.Domain.BusinessObjects;

namespace GameStore.DataAccess.MSSQL.Repositories
{
    public class UserRepository : GenericDataRepository<UserEntity, User>
    {
        private readonly IRoleRepository _roleRepository;
        public UserRepository(GamesSqlContext context, IMapper mapper, IRoleRepository roleRepository) : base(context, mapper)
        {
            _roleRepository = roleRepository;
        }

        public override void Add(User domainUser)
        {
            if (domainUser != null)
            {
                var entityUser = _mapper.Map<User, UserEntity>(domainUser);
                entityUser.Roles = _roleRepository.GetRoles(domainUser.Roles).ToList();
                entityUser.IsSqlEntity = true;
                _dbSet.Add(entityUser);
            }
        }

        public override void Update(User domainUser)
        {
            if (domainUser != null)
            {
                var entityUser = _mapper.Map<User, UserEntity>(domainUser);
                entityUser.Roles = _roleRepository.GetRoles(domainUser.Roles).ToList();
                entityUser.IsSqlEntity = true;
                var existingUser = _dbSet.Include(x => x.Roles).First(x => x.Id == domainUser.Id);
                var deletedRoles = existingUser.Roles.Except(entityUser.Roles, new IdEntityComparer<RoleEntity>());
                var addedRoles = entityUser.Roles.Except(existingUser.Roles, new IdEntityComparer<RoleEntity>());
                for (int i = 0; i < deletedRoles.Count(); i++)
                {
                    existingUser.Roles.Remove(deletedRoles.ElementAt(i));
                }

                foreach (var roleEntity in addedRoles)
                {
                    if (_context.Entry(roleEntity).State == EntityState.Detached)
                    {
                        _context.Roles.Attach(roleEntity);
                    }

                    existingUser.Roles.Add(roleEntity);
                }
                _context.SaveChanges();
                _context.Entry(_dbSet.Find(entityUser.Id)).State = EntityState.Detached;
                _context.Entry(entityUser).State = EntityState.Modified;
            }
        }
    }
}
