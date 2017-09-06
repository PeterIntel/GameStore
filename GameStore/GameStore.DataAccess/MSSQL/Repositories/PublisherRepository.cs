using System;
using System.Collections.Generic;
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

        public PublisherRepository(GamesSqlContext context, IMapper mapper, ICultureRepository cultureRepository) : base(context, mapper)
        {
            _cultureRepository = cultureRepository;
        }

        public override void Add(Publisher publisher)
        {
            if (publisher != null)
            {
                var publisherEntity = _mapper.Map<Publisher, PublisherEntity>(publisher);
                publisherEntity.IsSqlEntity = true;
                var id = Guid.NewGuid().ToString();
                publisherEntity.Locals.Add(new PublisherLocalEntity()
                {
                    Id = id,
                    Culture = _cultureRepository.GetCultureByCode(publisher.Locals.First().Culture.Code),
                    Description = publisher.Locals.First().Description
                });

                _dbSet.Add(publisherEntity);
            }
        }
    }
}
