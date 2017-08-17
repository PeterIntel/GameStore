using System.Collections.Generic;
using GameStore.DataAccess.MSSQL.Entities;
using GameStore.Domain.BusinessObjects;

namespace GameStore.DataAccess.Interfaces
{
    public interface IPlatformTypeRepository : IGenericDataRepository<PlatformTypeEntity, PlatformType>
    {
        IEnumerable<PlatformTypeEntity> GetPlatformTypes(IEnumerable<PlatformType> genres);
    }
}
