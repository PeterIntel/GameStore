using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.DataAccess.Interfaces;
using GameStore.DataAccess.MSSQL.Entities;
using GameStore.DataAccess.MSSQL.Entities.Localization;
using GameStore.Domain.BusinessObjects;

namespace GameStore.DataAccess.MSSQL.Repositories
{
    public class PublisherRepository : GenericDataRepository<PublisherEntity, Publisher>
    {
        private readonly ICultureRepository _cultureRepository;

        public PublisherRepository(GamesSqlContext context, IMapper mapper,
            ICultureRepository cultureRepository) : base(context, mapper)
        {
            _cultureRepository = cultureRepository;
        }

        public override void Add(Publisher publisher)
        {
            if (publisher != null)
            {
                var publisherEntity = _mapper.Map<Publisher, PublisherEntity>(publisher);
                publisherEntity.IsSqlEntity = true;
                var id = GetGuidId();
                publisherEntity.Locals = new List<PublisherLocalEntity>()
                {
                    new PublisherLocalEntity()
                    {
                        Id = id,
                        Culture = _cultureRepository.GetCultureByCode(publisher.Locals.First().Culture.Code),
                        Description = publisher.Locals.First().Description
                    }
                };
                
                _dbSet.Add(publisherEntity);
            }
        }

        public override void Update(Publisher publisher)
        {
            var currentPublisher = _mapper.Map<Publisher, PublisherEntity>(publisher);
            var existingPublisher = _dbSet.Find(publisher.Id);
            _mapper.Map(currentPublisher, existingPublisher);

            var currentCulture = publisher.Locals.First().Culture.Code;
            var description = publisher.Locals.First().Description;

            var local = existingPublisher.Locals.FirstOrDefault(x => x.Culture.Code == currentCulture);

            if (local == null)
            {
                var id = GetGuidId();
                existingPublisher.Locals.Add(

                    new PublisherLocalEntity()
                    {
                        Id = id,
                        Culture = _cultureRepository.GetCultureByCode(currentCulture),
                        Description = description
                    }
                );
            }
            else
            {
                local.Description = description;
            }

            if (_context.Entry(existingPublisher).State == EntityState.Detached)
            {
                _context.Publishers.Attach(existingPublisher);
            }

            _context.Entry(existingPublisher).State = EntityState.Modified;
        }
    }
}
