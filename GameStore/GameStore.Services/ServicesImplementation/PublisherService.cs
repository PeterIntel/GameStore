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
    public class PublisherService : IPublisherService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PublisherService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Add(Publisher item)
        {
            _unitOfWork.PublisherRepository.Add(item);
            _unitOfWork.Save();
        }

        public IEnumerable<Publisher> Get(params Expression<Func<Publisher, object>>[] includeProperties)
        {
            return _unitOfWork.PublisherRepository.Get(includeProperties);
        }

        public Publisher GetPublisherByCompanyName(string companyName)
        {
            return _unitOfWork.PublisherRepository.GetPublisherByCompanyName(companyName);
        }

        public void Remove(string id)
        {
            _unitOfWork.PublisherRepository.Remove(id);
            _unitOfWork.Save();
        }

        public void Remove(Publisher item)
        {
            _unitOfWork.PublisherRepository.Remove(item);
            _unitOfWork.Save();
        }

        public void Update(Publisher item)
        {
            _unitOfWork.PublisherRepository.Update(item);
            _unitOfWork.Save();
        }
        public IEnumerable<Publisher> GetAllPublishersAndMarkSelected(IEnumerable<string> selecredPublishers)
        {
            IEnumerable<Publisher> publishers = _unitOfWork.PublisherRepository.Get().ToList();
            if (selecredPublishers != null)
            {
                foreach (var item in publishers)
                {
                    if (selecredPublishers.Contains(item.CompanyName))
                    {
                        item.IsChecked = true;
                    }
                }
            }
            return publishers;
        }
    }
}
