using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GameStore.DataAccess.Interfaces;
using GameStore.DataAccess.MSSQL.Entities;
using GameStore.DataAccess.MSSQL.Entities.Localization;
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

        public override void Add(PlatformType platform)
        {
            if (platform != null)
            {
                var platformEntity = _mapper.Map<PlatformType, PlatformTypeEntity>(platform);
                platformEntity.IsSqlEntity = true;
                var id = Guid.NewGuid().ToString();
                platformEntity.Locals.Add(new PlatformTypeLocalEntity()
                {
                    Id = id,
                    Culture = _context.Cultures.First(c => c.Code == platform.Locals.First().Culture.Code),
                    TypeName = platform.Locals.First().TypeName
                });

                _dbSet.Add(platformEntity);
            }
        }
    }
}
