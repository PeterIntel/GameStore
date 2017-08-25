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

                var existingUser = _dbSet.Include(x => x.Roles).First(x => x.Id == domainUser.Id);
                _mapper.Map(entityUser, existingUser);

                var deletedRoles = existingUser.Roles.Except(entityUser.Roles, new IdEntityComparer<RoleEntity>());
                var addedRoles = entityUser.Roles.Except(existingUser.Roles, new IdEntityComparer<RoleEntity>());
                for (int i = 0; i < deletedRoles.Count(); i++)
                {
                    existingUser.Roles.Remove(deletedRoles.ElementAt(i));
                }

                foreach (var roleEntity in addedRoles)
                {
                    existingUser.Roles.Add(roleEntity);
                }

                if (_context.Entry(existingUser).State == EntityState.Detached)
                {
                    _context.Users.Attach(existingUser);
                }

                _context.Entry(existingUser).State = EntityState.Modified;
            }
        }
    }
}
