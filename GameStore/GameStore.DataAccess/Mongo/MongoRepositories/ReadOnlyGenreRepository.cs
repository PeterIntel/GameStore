using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using GameStore.DataAccess.Mongo.DataProviders;
using GameStore.DataAccess.Mongo.MongoEntities;
using GameStore.Domain.BusinessObjects;
using MongoDB.Driver;

namespace GameStore.DataAccess.Mongo.MongoRepositories
{
    public class ReadOnlyGenreRepository : ReadOnlyGenericRepository<MongoCategoryEntity, Genre>
    {
        public ReadOnlyGenreRepository(GamesMongoContext context, IMapper mapper) : base(context, mapper)
        {

        }

        public override IEnumerable<Genre> Get()
        {
            IQueryable<MongoCategoryEntity> queryToEntity = Collection.AsQueryable();
            queryToEntity = queryToEntity.GetNestedEntities();
            var result = Mapper.Map<IQueryable<MongoCategoryEntity>, IEnumerable<Genre>>(queryToEntity);

            return result;
        }

        public override IEnumerable<Genre> Get(Expression<Func<Genre, bool>> filterToDomain)
        {
            IQueryable<MongoCategoryEntity> queryToEntity = Collection.AsQueryable();
            queryToEntity = queryToEntity.GetNestedEntities();
            var queryToDomain = Mapper.Map<IQueryable<MongoCategoryEntity>, IEnumerable<Genre>>(queryToEntity);
            if (filterToDomain != null)
            {
                var predicate = filterToDomain.Compile();
                queryToDomain = queryToDomain.Where(predicate);
            }

            return queryToDomain;
        }

        public override Genre First(Expression<Func<Genre, bool>> filter)
        {
            IQueryable<MongoCategoryEntity> queryToEntity = Collection.AsQueryable();
            queryToEntity = queryToEntity.GetNestedEntities();
            var queryToDomain = Mapper.Map<IQueryable<MongoCategoryEntity>, IEnumerable<Genre>>(queryToEntity);
            if (filter != null)
            {
                var predicate = filter.Compile();
                queryToDomain = queryToDomain.Where(predicate);
            }

            var game = queryToDomain.FirstOrDefault();

            return game;
        }
    }
}
