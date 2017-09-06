﻿using System.Collections.Generic;
using System.Linq;
using GameStore.DataAccess.Interfaces;
using GameStore.DataAccess.MSSQL.Entities;
using GameStore.DataAccess.UnitOfWork;
using GameStore.Domain.BusinessObjects;
using GameStore.Domain.ServicesInterfaces;
using GameStore.Logging.Loggers;
using GameStore.Services.Localization;

namespace GameStore.Services.ServicesImplementation
{
    public class PlatformTypeService : BasicService<PlatformTypeEntity, PlatformType>, IPlatformTypeService
    {
        private readonly IGenericDataRepository<PlatformTypeEntity, PlatformType> _platformTypeRepository;
        private readonly ICultureService _cultureService;

        public PlatformTypeService(IUnitOfWork unitOfWork, IGenericDataRepository<PlatformTypeEntity, PlatformType> platformTypeRepository, IMongoLogger<PlatformType> logger, ICultureService cultureService, ILocalizationProvider<PlatformType> localizationProvider) :
            base(platformTypeRepository, unitOfWork, logger, localizationProvider)
        {
            _platformTypeRepository = platformTypeRepository;
            _cultureService = cultureService;
        }

        public IEnumerable<PlatformType> GetAllPlatformTypesAndMarkSelected(IEnumerable<string> selecredPlatforms, string cultureCode)
        {
            IEnumerable<PlatformType> platforms = _platformTypeRepository.Get().ToList();

            foreach (var platform in platforms)
            {
                LocalizationProvider.Localize(platform, cultureCode);
            }

            if (selecredPlatforms != null)
            {
                foreach (var item in platforms)
                {
                    if (selecredPlatforms.Contains(item.TypeName))
                    {
                        item.IsChecked = true;
                    }
                }
            }

            return platforms;
        }
    }
}
