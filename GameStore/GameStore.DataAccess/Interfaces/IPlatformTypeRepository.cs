using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.DataAccess.MSSQL.Entities;
using GameStore.Domain.BusinessObjects;

namespace GameStore.DataAccess.MSSQL.Repositories
{
    public interface IPlatformTypeRepository : IGenericDataRepository<PlatformTypeEntity, PlatformType>
    {
        IEnumerable<PlatformTypeEntity> GetPlatformTypes(IList<string> genres);
    }
}
