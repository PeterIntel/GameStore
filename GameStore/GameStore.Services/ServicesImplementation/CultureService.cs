using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.DataAccess.Interfaces;
using GameStore.DataAccess.MSSQL.Entities.Localization;
using GameStore.DataAccess.UnitOfWork;
using GameStore.Domain.BusinessObjects.LocalizationObjects;
using GameStore.Domain.ServicesInterfaces;
using GameStore.Logging.Loggers;
using GameStore.Services.Localization;

namespace GameStore.Services.ServicesImplementation
{
    public class CultureService : BasicService<CultureEntity, Culture>, ICultureService
    {
        private readonly IGenericDataRepository<CultureEntity, Culture> _cultuRepository;

        public CultureService(IGenericDataRepository<CultureEntity, Culture> genericRepository, IUnitOfWork unitOfWork, IMongoLogger<Culture> logger,
            IGenericDataRepository<CultureEntity, Culture> cultuRepository, ILocalizationProvider<Culture> localizatorProvider) : 
            base(genericRepository, unitOfWork, logger, localizatorProvider)
        {
            _cultuRepository = cultuRepository;
        }

        public Culture GetByCultureCode(string cultureCode)
        {
            var culture = _cultuRepository.First(c => c.Code == cultureCode);
            CheckForNull(culture, "Culture not found");

            return culture;
        }
    }
}
