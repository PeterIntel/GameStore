using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GameStore.DataAccess.Infrastructure;
using GameStore.DataAccess.Interfaces;
using GameStore.Domain.BusinessObjects;

namespace GameStore.DataAccess.Decorators
{
    public class GenericDecoratorRepository<TSqlEntity, TMongoEntity, TDomain> : IGenericDataRepository<TSqlEntity, TDomain> where TSqlEntity : class where TMongoEntity : class where TDomain : BasicDomain
    {
        protected readonly IGenericDataRepository<TSqlEntity, TDomain> SqlDataRepository;
        protected readonly IReadOnlyGenericRepository<TMongoEntity, TDomain> MongoDataRepository;

        public GenericDecoratorRepository(IGenericDataRepository<TSqlEntity, TDomain> sqlDataRepository, IReadOnlyGenericRepository<TMongoEntity, TDomain> mongoDataRepository)
        {
            SqlDataRepository = sqlDataRepository;
            MongoDataRepository = mongoDataRepository;
        }

        public void Add(TDomain item)
        {
            SqlDataRepository.Add(item);
        }

        public virtual IEnumerable<TDomain> Get(params Expression<Func<TDomain, object>>[] includeProperties)
        {
            var result = SqlDataRepository.Get(includeProperties).Union(GetRequiredMongoCollection());
            return result;
        }

        public virtual IEnumerable<TDomain> Get(Expression<Func<TDomain, bool>> filter, params Expression<Func<TDomain, object>>[] includeProperties)
        {
            var result = SqlDataRepository.Get(filter, includeProperties).Union(GetRequiredMongoCollection(filter));
            return result;
        }

        public virtual int GetCountObject(Expression<Func<TDomain, bool>> filter)
        {
            return SqlDataRepository.GetCountObject(filter) + MongoDataRepository.GetCountObject(filter);
        }

        public virtual TDomain First(Expression<Func<TDomain, bool>> filter)
        {
            var domain = SqlDataRepository.First(filter) ?? MongoDataRepository.First(filter);

            return domain;
        }

        public virtual TDomain GetItemById(string id)
        {
            var domain = SqlDataRepository.GetItemById(id) ?? MongoDataRepository.GetItemById(id);

            return domain;
        }

        public void Remove(string id)
        {
            SqlDataRepository.Remove(id);
        }

        public void Remove(TDomain item)
        {
            SqlDataRepository.Remove(item);
        }

        public void Update(TDomain item)
        {
            SqlDataRepository.Update(item);
        }
        public IEnumerable<TDomain> LoadDomainEntities(IEnumerable<string> ids)
        {
            var domainEntities = new List<TDomain>();
            foreach (var id in ids)
            {
                var domainEntity = GetItemById(id);
                if (domainEntity != null) { domainEntities.Add(domainEntity); }
            }

            return domainEntities;
        }

        public bool Any(Expression<Func<TDomain, bool>> filter)
        {
            return SqlDataRepository.Any(filter);
        }

        private IEnumerable<TDomain> GetRequiredMongoCollection()
        {
            var sqlIds = SqlDataRepository.Get().Select(sql => sql.Id);
            //Entities from Mongo which already added to SQL
            var mongoEntities = from i in sqlIds
                join j in MongoDataRepository.Get() on i equals j.Id
                select j;

            var requiredMongoCollection = MongoDataRepository.Get().Except(mongoEntities, new IdDomainComparer<TDomain>());

            return requiredMongoCollection;
        }

        private IEnumerable<TDomain> GetRequiredMongoCollection(Expression<Func<TDomain, bool>> filter)
        {
            // if selected platform type than show nothing from Mongo database
            if (filter.Body.ToString().Contains("PlatformTypes"))
            {
                filter = x => false;
            }

            var sqlIds = SqlDataRepository.Get(filter).Select(sql => sql.Id);

            //Entities from Mongo which already added to SQL
            var mongoEntities = from i in sqlIds
                join j in MongoDataRepository.Get() on i equals j.Id
                select j;

            var requiredMongoCollection = MongoDataRepository.Get().Except(mongoEntities, new IdDomainComparer<TDomain>()).AsQueryable().Where(filter);

            return requiredMongoCollection;
        }
    }
}
