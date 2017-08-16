using GameStore.DataAccess.Mongo.MongoEntities;
using GameStore.Domain.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using GameStore.DataAccess.Interfaces;
using GameStore.DataAccess.Mongo.DataProviders;
using MongoDB.Driver;

namespace GameStore.DataAccess.Mongo.MongoRepositories
{
    public class ReadOnlyGameRepository : ReadOnlyGenericRepository<MongoProductEntity, Game>, IReadOnlyGameRepository

    {
        public ReadOnlyGameRepository(GamesMongoContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override IEnumerable<Game> Get()
        {
            IQueryable<MongoProductEntity> queryToEntity = Collection.AsQueryable();
            queryToEntity = queryToEntity.GetNestedEntities();
            var result = queryToEntity.ProjectTo<Game>(Mapper.ConfigurationProvider);

            return result;
        }

        public override IEnumerable<Game> Get(Expression<Func<Game, bool>> filterToDomain)
        {
            IQueryable<MongoProductEntity> queryToEntity = Collection.AsQueryable();
            if (filterToDomain != null)
            {
                var filterToEntity = Mapper.Map<Expression<Func<Game, bool>>, Expression<Func<MongoProductEntity, bool>>>(filterToDomain);
                queryToEntity = queryToEntity.Where(filterToEntity);
            }
            var f = queryToEntity.ToList();
            queryToEntity = queryToEntity.GetNestedEntities();
            var fs = queryToEntity.ToList();
            var queryToDomain = queryToEntity.ProjectTo<Game>(Mapper.ConfigurationProvider);
            var fa = queryToDomain.ToList();

            return queryToDomain;
        }

        public override Game First(Expression<Func<Game, bool>> filter)
        {
            IQueryable<MongoProductEntity> queryToEntity = Collection.AsQueryable();

            if (filter != null)
            {
                var filterToEntity = Mapper.Map<Expression<Func<Game, bool>>, Expression<Func<MongoProductEntity, bool>>>(filter);
                queryToEntity = queryToEntity.Where(filterToEntity);
            }

            queryToEntity = queryToEntity.GetNestedEntities();
            Game order = queryToEntity.ProjectTo<Game>(Mapper.ConfigurationProvider).FirstOrDefault();

            return order;
        }

        public new IEnumerable<Game> Get<TKey>(Expression<Func<Game, bool>> filterDomain, Expression<Func<Game, TKey>> sortDomain, bool ascending = true, int page = 1, int? size = 10)
        {
            IQueryable<MongoProductEntity> queryToEntity = Collection.AsQueryable();

            var sortEntity = Mapper.Map<Expression<Func<Game, TKey>>, Expression<Func<MongoProductEntity, TKey>>>(sortDomain);

            if (filterDomain != null)
            {
                var filterToEntity = Mapper.Map<Expression<Func<Game, bool>>, Expression<Func<MongoProductEntity, bool>>>(filterDomain);
                queryToEntity = queryToEntity.Where(filterToEntity);
            }

            queryToEntity = ascending ? queryToEntity.OrderBy(sortEntity) : queryToEntity.OrderByDescending(sortEntity);

            queryToEntity = queryToEntity.Take((int)size * page);

            queryToEntity = queryToEntity.GetNestedEntities();
            var queryToDomain = queryToEntity.ProjectTo<Game>(Mapper.ConfigurationProvider);

            return queryToDomain;
        }
    }
}
