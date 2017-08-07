using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GameStore.DataAccess.MSSQL.Entities;
using GameStore.Domain.BusinessObjects;

namespace GameStore.DataAccess.MSSQL.Repositories
{
    public class PlatformTypeRepository : GenericDataRepository<PlatformTypeEntity, PlatformType>, IPlatformTypeRepository
    {
        public PlatformTypeRepository(GamesSqlContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public IEnumerable<PlatformTypeEntity> GetPlatformTypes(IList<string> platformtypes)
        {
            var platforms = from i in platformtypes
                from platform in _dbSet
                where i == platform.TypeName
                select platform;
            return platforms;
        }
    }
}
