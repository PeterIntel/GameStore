using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using GameStore.DataAccess.Mongo.DataProviders;
using GameStore.DataAccess.Mongo.MongoEntities;
using GameStore.Domain.BusinessObjects;
using MongoDB.Driver;

namespace GameStore.DataAccess.Mongo.MongoRepositories
{
    public class ReadOnlyOrderRepository : ReadOnlyGenericRepository<MongoOrderEntity, Order>
    {
        public ReadOnlyOrderRepository(GamesMongoContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override IEnumerable<Order> Get()
        {
            IQueryable<MongoOrderEntity> queryToEntity = Collection.AsQueryable();
            queryToEntity = queryToEntity.GetNestedEntities();
            var queryToDomain = Mapper.Map<IQueryable<MongoOrderEntity>, IEnumerable<Order>>(queryToEntity);

            return queryToDomain;
        }

        public override IEnumerable<Order> Get(Expression<Func<Order, bool>> filterToDomain)
        {
            IQueryable<MongoOrderEntity> queryToEntity = Collection.AsQueryable();
            queryToEntity = queryToEntity.GetNestedEntities();
            var queryToDomain = Mapper.Map<IQueryable<MongoOrderEntity>, IEnumerable<Order>>(queryToEntity);

            if (filterToDomain != null)
            {
                var predicate = filterToDomain.Compile();
                queryToDomain = queryToDomain.Where(predicate);
            }

            return queryToDomain;
        }

        public override Order First(Expression<Func<Order, bool>> filter)
        {
            IQueryable<MongoOrderEntity> queryToEntity = Collection.AsQueryable();
            queryToEntity = queryToEntity.GetNestedEntities();
            var queryToDomain = Mapper.Map<IQueryable<MongoOrderEntity>, IEnumerable<Order>>(queryToEntity);

            if (filter != null)
            {
                var predicate = filter.Compile();
                queryToDomain = queryToDomain.Where(predicate);
            }

            var order = queryToDomain.FirstOrDefault();

            return order;
        }
    }
}
