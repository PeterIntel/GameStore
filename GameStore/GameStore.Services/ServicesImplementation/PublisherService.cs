using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GameStore.DataAccess.Decorators;
using GameStore.DataAccess.Mongo.MongoEntities;
using GameStore.DataAccess.MSSQL.Entities;
using GameStore.DataAccess.UnitOfWork;
using GameStore.Domain.BusinessObjects;
using GameStore.Domain.ServicesInterfaces;

namespace GameStore.Services.ServicesImplementation
{
    public class PublisherService : IPublisherService
    {
        private readonly IGenericDecoratorRepository<PublisherEntity, MongoSupplierEntity, Publisher> _publisherRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PublisherService(IUnitOfWork unitOfWork, IGenericDecoratorRepository<PublisherEntity, MongoSupplierEntity, Publisher> publisherRepository)
        {
            _publisherRepository = publisherRepository;
            _unitOfWork = unitOfWork;
        }
        public void Add(Publisher item)
        {
            _publisherRepository.Add(item);
            _unitOfWork.Save();
        }

        public IEnumerable<Publisher> Get(params Expression<Func<Publisher, object>>[] includeProperties)
        {
            return _publisherRepository.Get(includeProperties).ToList();
        }

        public Publisher GetPublisherByCompanyName(string companyName)
        {
            return _publisherRepository.GetFirst(x => x.CompanyName == companyName);
        }

        public void Remove(string id)
        {
            _publisherRepository.Remove(id);
            _unitOfWork.Save();
        }

        public void Remove(Publisher item)
        {
            _publisherRepository.Remove(item);
            _unitOfWork.Save();
        }

        public void Update(Publisher item)
        {
            _publisherRepository.Update(item);
            _unitOfWork.Save();
        }
        public IEnumerable<Publisher> GetAllPublishersAndMarkSelected(IEnumerable<string> selecredPublishers)
        {
            IEnumerable<Publisher> publishers = _publisherRepository.Get().ToList();
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
