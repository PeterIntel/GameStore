using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GameStore.DataAccess.Interfaces;
using GameStore.DataAccess.MSSQL.Entities;
using GameStore.Domain.BusinessObjects;

namespace GameStore.DataAccess.MSSQL.Repositories
{
    public class PlatformTypeRepository : GenericDataRepository<PlatformTypeEntity, PlatformType>, IPlatformTypeRepository
    {
        public PlatformTypeRepository(GamesSqlContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public IEnumerable<PlatformTypeEntity> GetPlatformTypes(IEnumerable<PlatformType> platformtypes)
        {
            var platforms = from i in platformtypes
                from platform in _dbSet
                where i.TypeName == platform.Id
                select platform;
            return platforms;
        }
    }
}
