using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GameStore.DataAccess.UnitOfWork;
using GameStore.Domain.BusinessObjects;
using GameStore.Domain.ServicesInterfaces;

namespace GameStore.Services.ServicesImplementation
{
    public class PlatformTypeService : IPlatformTypeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlatformTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Add(PlatformType item)
        {
            _unitOfWork.PlatformTypeRepository.Add(item);
            _unitOfWork.Save();
        }

        public IEnumerable<PlatformType> Get(params Expression<Func<PlatformType, object>>[] includeProperties)
        {
            return _unitOfWork.PlatformTypeRepository.Get(includeProperties);
        }

        public void Remove(int id)
        {
            _unitOfWork.PlatformTypeRepository.Remove(id);
            _unitOfWork.Save();
        }

        public void Remove(PlatformType item)
        {
            _unitOfWork.PlatformTypeRepository.Remove(item);
            _unitOfWork.Save();
        }

        public void Update(PlatformType item)
        {
            _unitOfWork.PlatformTypeRepository.Update(item);
            _unitOfWork.Save();
        }

        public IEnumerable<PlatformType> GetAllPlatformTypesAndMarkSelected(IEnumerable<string> selecredPlatforms)
        {
            IEnumerable<PlatformType> platforms = _unitOfWork.PlatformTypeRepository.Get().ToList();
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
