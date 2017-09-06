using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using GameStore.DataAccess.Interfaces;
using GameStore.DataAccess.Mongo.DataProviders;
using GameStore.DataAccess.Mongo.MongoEntities;
using GameStore.Domain.BusinessObjects;
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
            var result = Mapper.Map<IQueryable<MongoProductEntity>, IEnumerable<Game>>(queryToEntity);

            return result;
        }

        public override IEnumerable<Game> Get(Expression<Func<Game, bool>> filterToDomain)
        {
            IQueryable<MongoProductEntity> queryToEntity = Collection.AsQueryable();
            queryToEntity = queryToEntity.GetNestedEntities();
            var queryToDomain = Mapper.Map<IQueryable<MongoProductEntity>, IEnumerable<Game>>(queryToEntity);
            if (filterToDomain != null)
            {
                var predicate = filterToDomain.Compile();
                queryToDomain = queryToDomain.Where(predicate);
            }

            return queryToDomain;
        }

        public override Game First(Expression<Func<Game, bool>> filter)
        {
            IQueryable<MongoProductEntity> queryToEntity = Collection.AsQueryable();
            queryToEntity = queryToEntity.GetNestedEntities();
            var queryToDomain = Mapper.Map<IQueryable<MongoProductEntity>, IEnumerable<Game>>(queryToEntity);
            if (filter != null)
            {
                var predicate = filter.Compile();
                queryToDomain = queryToDomain.Where(predicate);
            }

            var game = queryToDomain.FirstOrDefault();

            return game;
        }

        public IEnumerable<Game> Get<TKey>(Expression<Func<Game, bool>> filterDomain, Expression<Func<Game, TKey>> sortDomain, bool ascending = true, int page = 1, int? size = 10)
        {
            IQueryable<MongoProductEntity> queryToEntity = Collection.AsQueryable();
            queryToEntity = queryToEntity.GetNestedEntities();
            var queryToDomain = Mapper.Map<IQueryable<MongoProductEntity>, IEnumerable<Game>>(queryToEntity);

            if (filterDomain != null)
            {
                var predicate = filterDomain.Compile();
                queryToDomain = queryToDomain.Where(predicate);
            }

            var sortPredicate = sortDomain.Compile();

            queryToDomain = ascending ? queryToDomain.OrderBy(sortPredicate) : queryToDomain.OrderByDescending(sortPredicate);

            queryToDomain = queryToDomain.Take((int)size * page);

            return queryToDomain;
        }
    }
}
