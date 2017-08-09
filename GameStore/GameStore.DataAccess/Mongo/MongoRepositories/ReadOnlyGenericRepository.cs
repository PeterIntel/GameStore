using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using GameStore.DataAccess.Interfaces;
using GameStore.DataAccess.Mongo.MongoEntities;
using GameStore.DataAccess.MSSQL.Entities;
using GameStore.DataAccess.MSSQL.Migrations;
using GameStore.Domain.BusinessObjects;
using MongoDB.Bson;
using MongoDB.Driver;
using GameStore.DataAccess.Mongo.DataProviders;

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
        public IEnumerable<TDomain> Get(params string[] ids)
        {
            IQueryable<TEntity> queryToEntity;
            //if (ids != null)
            //{
            //    var filter = Builders<TEntity>.Filter.Where(x => ids.Contains(x.Id));
            //    queryToEntity =  Collection.Find(filter).
            //}
            queryToEntity = Collection.AsQueryable();
            queryToEntity = queryToEntity.GetChildren<TEntity>();
            var result = queryToEntity.AsQueryable().ProjectTo<TDomain>(Mapper.ConfigurationProvider);
            return result;
        }

        public IEnumerable<TDomain> Get(Expression<Func<TDomain, bool>> filterDomain, params string[] ids)
        {
            IQueryable<TEntity> queryToEntity = Collection.AsQueryable();
            queryToEntity = queryToEntity.GetChildren<TEntity>();
            var queryToDomain = queryToEntity.AsQueryable().ProjectTo<TDomain>(Mapper.ConfigurationProvider);
            queryToDomain = queryToDomain.Where(filterDomain);
            var f = queryToDomain.ToList();
            return queryToDomain;
        }

        public int GetCountObject(Expression<Func<TDomain, bool>> filter)
        {
            var domainItems = Collection.AsQueryable().ProjectTo<TDomain>(Mapper.ConfigurationProvider);

            if (filter != null)
            {
                domainItems = domainItems.Where(filter);
            }

            var result = domainItems.Count();
            return result;
        }

        public TDomain GetItemById(string id)
        {
            var filter = Builders<TEntity>.Filter.Eq("_id", id);
            TEntity entity = Collection.Find(filter).FirstOrDefault();

            if (entity != null)
            {
                TDomain domain = Mapper.Map<TEntity, TDomain>(entity);
                return domain;
            }

            return null;
        }

        public TDomain GetFirst(Expression<Func<TDomain, bool>> filter)
        {
            var filterToEntity = Mapper.Map<Expression<Func<TDomain, bool>>, Expression<Func<TEntity, bool>>>(filter);

            if (filter != null)
            {
                TDomain domain = Collection.AsQueryable().Where(filterToEntity).ProjectTo<TDomain>(Mapper.ConfigurationProvider).FirstOrDefault();
                return domain;
            }

            return null;
        }
    }
}
