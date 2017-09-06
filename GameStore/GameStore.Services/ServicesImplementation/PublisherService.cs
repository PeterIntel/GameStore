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
    public class PublisherService : BasicService<PublisherEntity, Publisher>, IPublisherService
    {
        private readonly IGenericDataRepository<PublisherEntity, Publisher> _publisherRepository;

        public PublisherService(IUnitOfWork unitOfWork, IGenericDataRepository<PublisherEntity, Publisher> publisherRepository, IMongoLogger<Publisher> logger) : base(publisherRepository, unitOfWork, logger)
        {
            _publisherRepository = publisherRepository;
        }

        public Publisher GetPublisherByCompanyName(string companyName)
        {
            return _publisherRepository.First(x => x.CompanyName == companyName);
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
