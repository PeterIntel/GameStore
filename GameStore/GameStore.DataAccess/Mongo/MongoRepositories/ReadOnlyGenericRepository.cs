using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using GameStore.DataAccess.Interfaces;
using GameStore.DataAccess.Mongo.MongoEntities;
using GameStore.Domain.BusinessObjects;
using MongoDB.Driver;

namespace GameStore.DataAccess.Mongo.MongoRepositories
{
    public class ReadOnlyGenericRepository<TEntity, TDomain> : IReadOnlyGenericRepository<TEntity, TDomain> where TEntity : BasicMongoEntity where TDomain : BasicDomain
    {
        protected GamesMongoContext Context;
        protected IMongoCollection<TEntity> Collection;
        protected IMapper Mapper;

        public ReadOnlyGenericRepository(GamesMongoContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
            Collection = Context.GetCollection<TEntity>();
        }
        public virtual IEnumerable<TDomain> Get()
        {
            IQueryable<TEntity> queryToEntity = Collection.AsQueryable();
            var queryToDomain = Mapper.Map<IQueryable<TEntity>, IEnumerable<TDomain>>(queryToEntity);

            return queryToDomain;
        }

        public virtual IEnumerable<TDomain> Get(Expression<Func<TDomain, bool>> filterToDomain)
        {
            IQueryable<TEntity> queryToEntity = Collection.AsQueryable();
            var queryToDomain = Mapper.Map<IQueryable<TEntity>, IEnumerable<TDomain>>(queryToEntity);

            if (filterToDomain != null)
            {
                var predicate = filterToDomain.Compile();
                queryToDomain = queryToDomain.Where(predicate);
            }

            return queryToDomain;
        }

        public virtual int GetCountObject(Expression<Func<TDomain, bool>> filter)
        {
            IEnumerable<TEntity> queryToEntity = Collection.AsQueryable();
            var queryToDomain = Mapper.Map<IEnumerable<TEntity>, IEnumerable<TDomain>>(queryToEntity);

            if (filter != null)
            {
                queryToDomain = queryToDomain.AsQueryable().Where(filter);
            }

            var result = queryToDomain.ToList().Count();

            return result;
        }

        public TDomain GetItemById(string id)
        {
            var filter = Builders<TEntity>.Filter.Eq("Id", id);
            TEntity entity = Collection.Find(filter).FirstOrDefault();

            if (entity != null)
            {
                TDomain domain = Mapper.Map<TEntity, TDomain>(entity);
                return domain;
            }

            return null;
        }

        public virtual TDomain First(Expression<Func<TDomain, bool>> filter)
        {
            IQueryable<TEntity> queryToEntity = Collection.AsQueryable();
            var queryToDomain = Mapper.Map<IEnumerable<TEntity>, IEnumerable<TDomain>>(queryToEntity.ToList());

            if (filter != null)
            {
                var predicate = filter.Compile();
                queryToDomain = queryToDomain.Where(predicate);
            }

            var result = queryToDomain.FirstOrDefault();

            return result;
        }
    }
}
