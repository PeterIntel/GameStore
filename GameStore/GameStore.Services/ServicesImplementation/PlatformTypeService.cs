using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GameStore.DataAccess.Interfaces;
using GameStore.DataAccess.MSSQL.Entities;
using GameStore.DataAccess.UnitOfWork;
using GameStore.Domain.BusinessObjects;
using GameStore.Domain.ServicesInterfaces;

namespace GameStore.Services.ServicesImplementation
{
    public class PlatformTypeService : IPlatformTypeService
    {
        private readonly IGenericDataRepository<PlatformTypeEntity, PlatformType> _platformTypeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PlatformTypeService(IUnitOfWork unitOfWork, IGenericDataRepository<PlatformTypeEntity, PlatformType> platformTypeRepository)
        {
            _unitOfWork = unitOfWork;
            _platformTypeRepository = platformTypeRepository;
        }
        public void Add(PlatformType item)
        {
            _platformTypeRepository.Add(item);
            _unitOfWork.Save();
        }

        public IEnumerable<PlatformType> Get(params Expression<Func<PlatformType, object>>[] includeProperties)
        {
            return _platformTypeRepository.Get(includeProperties);
        }

        public void Remove(string id)
        {
            _platformTypeRepository.Remove(id);
            _unitOfWork.Save();
        }

        public void Remove(PlatformType item)
        {
            _platformTypeRepository.Remove(item);
            _unitOfWork.Save();
        }

        public void Update(PlatformType item)
        {
            _platformTypeRepository.Update(item);
            _unitOfWork.Save();
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
