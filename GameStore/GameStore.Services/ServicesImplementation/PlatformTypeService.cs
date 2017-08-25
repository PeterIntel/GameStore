using System.Collections.Generic;
using System.Linq;
using GameStore.DataAccess.Interfaces;
using GameStore.DataAccess.MSSQL.Entities;
using GameStore.DataAccess.UnitOfWork;
using GameStore.Domain.BusinessObjects;
using GameStore.Domain.ServicesInterfaces;
using GameStore.Logging.Loggers;

namespace GameStore.Services.ServicesImplementation
{
    public class PlatformTypeService : BasicService<PlatformTypeEntity, PlatformType>, IPlatformTypeService
    {
        private readonly IGenericDataRepository<PlatformTypeEntity, PlatformType> _platformTypeRepository;

        public PlatformTypeService(IUnitOfWork unitOfWork, IGenericDataRepository<PlatformTypeEntity, PlatformType> platformTypeRepository, IMongoLogger<PlatformType> logger) : base(platformTypeRepository, unitOfWork, logger)
        {
            _platformTypeRepository = platformTypeRepository;
        }

        public IEnumerable<PlatformType> GetAllPlatformTypesAndMarkSelected(IEnumerable<string> selecredPlatforms)
        {
            IEnumerable<PlatformType> platforms = _platformTypeRepository.Get().ToList();
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
