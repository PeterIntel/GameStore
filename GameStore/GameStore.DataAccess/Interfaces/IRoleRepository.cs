using System.Collections.Generic;
using GameStore.DataAccess.MSSQL.Entities;
using GameStore.Domain.BusinessObjects;

namespace GameStore.DataAccess.Interfaces
{
    public interface IRoleRepository : IGenericDataRepository<RoleEntity, Role>
    {
        IEnumerable<RoleEntity> GetRoles(IEnumerable<Role> roles);
    }
}
