using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using GameStore.DataAccess.Mongo.MongoEntities;
using GameStore.DataAccess.MSSQL.Entities;
using GameStore.DataAccess.MSSQL.Migrations;
using GameStore.Domain.BusinessObjects;
using MongoDB.Bson;
using MongoDB.Driver;
using GameStore.DataAccess.Mongo.DataProviders;

namespace GameStore.DataAccess.Mongo.MongoRepositories
{
    public class ReadOnlyGenericRepository<TEntity, TDomain> : IReadOnlyGenericRepository<TEntity, TDomain> where TEntity : class where TDomain : class
    {
        protected GamesMongoContext _context;
        protected IMongoCollection<TEntity> _collection;
        protected IMapper _mapper;

        public ReadOnlyGenericRepository(GamesMongoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _collection = _context.GetCollection<TEntity>();
        }
        public IEnumerable<TDomain> Get()
        {
            IQueryable<TEntity> queryToEntity = _collection.AsQueryable();
            queryToEntity = queryToEntity.GetChildren<TEntity>();
            var result = queryToEntity.AsQueryable().ProjectTo<TDomain>(_mapper.ConfigurationProvider);
            var res = queryToEntity.ToList();
            return result;
        }

        public IEnumerable<TDomain> Get(Expression<Func<TDomain, bool>> filterDomain)
        {
            IQueryable<TEntity> queryToEntity = _collection.AsQueryable();
            queryToEntity = queryToEntity.GetChildren<TEntity>();
            var queryToDomain = queryToEntity.AsQueryable().ProjectTo<TDomain>(_mapper.ConfigurationProvider);
            queryToDomain = queryToDomain.Where(filterDomain);
            return queryToDomain;
        }

        public int GetCountObject(Expression<Func<TDomain, bool>> filter)
        {
            var domainItems = _collection.AsQueryable().ProjectTo<TDomain>(_mapper.ConfigurationProvider);

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
            TEntity entity = _collection.Find(filter).FirstOrDefault();

            if (entity != null)
            {
                TDomain domain = _mapper.Map<TEntity, TDomain>(entity);
                return domain;
            }

            return null;
        }
    }
}
