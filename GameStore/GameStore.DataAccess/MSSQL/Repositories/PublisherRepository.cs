using System;
using System.Linq;
using AutoMapper;
using GameStore.DataAccess.MSSQL.Entities;
using GameStore.Domain.BusinessObjects;

namespace GameStore.DataAccess.MSSQL.Repositories
{
    public class PublisherRepository : GenericDataRepository<PublisherEntity, Publisher>, IPublisherRepository
    {
        public PublisherRepository(GamesSqlContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public Publisher GetPublisherByCompanyName(string key)
        {
            PublisherEntity entity;
            try
            {
                var listPublisherEntities = _dbSet.Where(x => x.CompanyName == key && x.IsDeleted == false);
                entity = listPublisherEntities.First();
            }
            catch (Exception ex)
            {
                throw new Exception($"Publisher with the company name {key} wasn't found!", ex);
            }
            return _mapper.Map<PublisherEntity, Publisher>(entity);
        }
    }
}
