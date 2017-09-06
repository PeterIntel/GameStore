using System;
using System.Collections.Generic;
using System.Linq;
using GameStore.DataAccess.Interfaces;
using GameStore.DataAccess.MSSQL.Entities;
using GameStore.DataAccess.UnitOfWork;
using GameStore.Domain.BusinessObjects;
using GameStore.Domain.BusinessObjects.LocalizationObjects;
using GameStore.Domain.ServicesInterfaces;
using GameStore.Logging.Loggers;
using GameStore.Services.Localization;

namespace GameStore.Services.ServicesImplementation
{
    public class PublisherService : BasicService<PublisherEntity, Publisher>, IPublisherService
    {
        private readonly IGenericDataRepository<PublisherEntity, Publisher> _publisherRepository;

        public PublisherService(IUnitOfWork unitOfWork, IGenericDataRepository<PublisherEntity, Publisher> publisherRepository, IMongoLogger<Publisher> logger,
            ILocalizationProvider<Publisher> localizationProvider) : base(publisherRepository, unitOfWork, logger, localizationProvider)
        {
            _publisherRepository = publisherRepository;
        }

        public Publisher GetPublisherByCompanyName(string companyName, string cultureCode)
        {
            var company = _publisherRepository.First(x => x.CompanyName == companyName);
            LocalizationProvider.Localize(company, cultureCode);

            return company;
        }

        public IEnumerable<Publisher> GetAllPublishersAndMarkSelected(IEnumerable<string> selecredPublishers, string cultureCode)
        {
            IEnumerable<Publisher> publishers = _publisherRepository.Get().ToList();

            foreach (var publisher in publishers)
            {
                LocalizationProvider.Localize(publisher, cultureCode);
            }

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

        public override void Add(Publisher publisher, string cultureCode)
        {
            publisher.Locals = new List<PublisherLocal>()
            {
                new PublisherLocal()
                {
                    Culture = new Culture() {Code = cultureCode},
                    Description = publisher.Description
                }
            };
            
            base.Add(publisher, cultureCode);
        }

        public override void Update(Publisher publisher, string cultureCode)
        {
            publisher.Locals = new List<PublisherLocal>()
            {
                new PublisherLocal()
                {
                    Culture = new Culture() {Code = cultureCode},
                    Description = publisher.Description
                }
            };

            base.Update(publisher, cultureCode);
        }
    }
}
