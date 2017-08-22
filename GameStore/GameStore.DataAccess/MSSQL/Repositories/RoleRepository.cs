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
    public class RoleRepository : GenericDataRepository<RoleEntity, Role>, IRoleRepository
    {
        public RoleRepository(GamesSqlContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public IEnumerable<RoleEntity> GetRoles(IEnumerable<Role> roles)
        {
            var entityRoles = from role in roles
                from entityRole in _dbSet
                where role.RoleEnum == entityRole.Role
                select entityRole;

            return entityRoles;
        }
    }
}
