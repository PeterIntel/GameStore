using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
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
    }
}
