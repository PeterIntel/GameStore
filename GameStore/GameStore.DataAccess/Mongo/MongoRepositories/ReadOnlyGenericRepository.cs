﻿using System;
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
        public virtual IEnumerable<TDomain> Get()
        {
            IQueryable<TEntity> queryToEntity = Collection.AsQueryable();
            var result = queryToEntity.ProjectTo<TDomain>(Mapper.ConfigurationProvider);
            
            return result;
        }

        public virtual IEnumerable<TDomain> Get(Expression<Func<TDomain, bool>> filterToDomain)
        {
            IQueryable<TEntity> queryToEntity = Collection.AsQueryable();
            if (filterToDomain != null)
            {
                var filterToEntity = Mapper.Map<Expression<Func<TDomain, bool>>, Expression<Func<TEntity, bool>>>(filterToDomain);
                queryToEntity = queryToEntity.Where(filterToEntity);
            }
            var queryToDomain = queryToEntity.ProjectTo<TDomain>(Mapper.ConfigurationProvider);
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

            if (filter != null)
            {
                var filterToEntity = Mapper.Map<Expression<Func<TDomain, bool>>, Expression<Func<TEntity, bool>>>(filter);
                queryToEntity = queryToEntity.Where(filterToEntity); 
            }
            TDomain domain = queryToEntity.ProjectTo<TDomain>(Mapper.ConfigurationProvider).FirstOrDefault();
            return domain;
        }
    }
}
