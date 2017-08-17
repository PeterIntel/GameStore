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
using GameStore.Logging.Loggers;

namespace GameStore.Services.ServicesImplementation
{
    public class PublisherService : BasicService<Publisher>, IPublisherService
    {
        private readonly IGenericDecoratorRepository<PublisherEntity, MongoSupplierEntity, Publisher> _publisherRepository;
        private readonly IMongoLogger<Publisher> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public PublisherService(IUnitOfWork unitOfWork, IGenericDecoratorRepository<PublisherEntity, MongoSupplierEntity, Publisher> publisherRepository, IMongoLogger<Publisher> logger)
        {
            _publisherRepository = publisherRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public void Add(Publisher item)
        {
            AssignIdIfEmpty(item);
            _publisherRepository.Add(item);
            _unitOfWork.Save();
            _logger.Write(Operation.Insert, item);
        }

        public IEnumerable<Publisher> Get(params Expression<Func<Publisher, object>>[] includeProperties)
        {
            return _publisherRepository.Get(includeProperties).ToList();
        }

        public Publisher GetPublisherByCompanyName(string companyName)
        {
            return _publisherRepository.First(x => x.CompanyName == companyName);
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
            _logger.Write(Operation.Delete, item);
        }

        public void Update(Publisher item)
        {
            _publisherRepository.Update(item);
            _unitOfWork.Save();
            var updatedPublisher = _publisherRepository.GetItemById(item.Id);
            _logger.Write(Operation.Update, item, updatedPublisher);
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
