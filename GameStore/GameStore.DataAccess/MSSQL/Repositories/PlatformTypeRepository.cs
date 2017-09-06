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
        private readonly ICultureRepository _cultureRepository;
        public PlatformTypeRepository(GamesSqlContext context, IMapper mapper, ICultureRepository cultureRepository) : base(context, mapper)
        {
            _cultureRepository = cultureRepository;
        }

        public IEnumerable<PlatformTypeEntity> GetPlatformTypes(IEnumerable<PlatformType> platformtypes)
        {
            var platforms = from i in platformtypes
                            from platform in _dbSet
                            where i.Id == platform.Id
                            select platform;
            return platforms;
        }

        public override void Add(PlatformType platform)
        {
            if (platform != null)
            {
                var platformEntity = _mapper.Map<PlatformType, PlatformTypeEntity>(platform);
                platformEntity.IsSqlEntity = true;
                var id = GetGuidId();
                platformEntity.Locals = new List<PlatformTypeLocalEntity>() {
                    new PlatformTypeLocalEntity()
                    {
                        Id = id,
                        Culture = _cultureRepository.GetCultureByCode(platform.Locals.First().Culture.Code),
                        TypeName = platform.Locals.First().TypeName
                    }
                };

                _dbSet.Add(platformEntity);
            }
        }
    }
}
