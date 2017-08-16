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
            var result = queryToEntity.ProjectTo<Order>(Mapper.ConfigurationProvider);

            return result;
        }

        public override IEnumerable<Order> Get(Expression<Func<Order, bool>> filterToDomain)
        {
            IQueryable<MongoOrderEntity> queryToEntity = Collection.AsQueryable();
            if (filterToDomain != null)
            {
                var filterToEntity = Mapper.Map<Expression<Func<Order, bool>>, Expression<Func<MongoOrderEntity, bool>>>(filterToDomain);
                queryToEntity = queryToEntity.Where(filterToEntity);
            }

            queryToEntity = queryToEntity.GetNestedEntities();
            var queryToDomain = queryToEntity.ProjectTo<Order>(Mapper.ConfigurationProvider);

            return queryToDomain;
        }

        public override Order First(Expression<Func<Order, bool>> filter)
        {
            IQueryable<MongoOrderEntity> queryToEntity = Collection.AsQueryable();

            if (filter != null)
            {
                var filterToEntity = Mapper.Map<Expression<Func<Order, bool>>, Expression<Func<MongoOrderEntity, bool>>>(filter);
                queryToEntity = queryToEntity.Where(filterToEntity);
            }

            queryToEntity = queryToEntity.GetNestedEntities();
            Order order = queryToEntity.ProjectTo<Order>(Mapper.ConfigurationProvider).FirstOrDefault();

            return order;
        }
    }
}
