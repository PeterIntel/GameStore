using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GameStore.DataAccess.Interfaces;
using GameStore.Domain.BusinessObjects;
using System.Text.RegularExpressions;
using GameStore.DataAccess.Infrastructure;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace GameStore.DataAccess.Decorators
{
    public class GenericDecoratorRepositoryRepository<TSqlEntity, TMongoEntity, TDomain> : IGenericDecoratorRepository<TSqlEntity, TMongoEntity, TDomain> where TSqlEntity : class where TMongoEntity : class where TDomain : BasicDomain
    {
        protected readonly IGenericDataRepository<TSqlEntity, TDomain> SqlDataRepository;
        protected readonly IReadOnlyGenericRepository<TMongoEntity, TDomain> MongoDataRepository;

        public GenericDecoratorRepositoryRepository(IGenericDataRepository<TSqlEntity, TDomain> sqlDataRepository, IReadOnlyGenericRepository<TMongoEntity, TDomain> mongoDataRepository)
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
            return SqlDataRepository.GetCountObject(filter) + GetRequiredMongoCollection(filter).Count();
        }

        public virtual TDomain GetFirst(Expression<Func<TDomain, bool>> filter)
        {
            var domain = SqlDataRepository.GetFirst(filter) ?? MongoDataRepository.GetFirst(filter);

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

        public IEnumerable<TDomain> GetRequiredMongoCollection()
        {
            var sqlIds = SqlDataRepository.Get().Select(sql => sql.Id);
            var requiredMongoCollection = MongoDataRepository.Get().Except((from i in sqlIds
                                                                            join j in MongoDataRepository.Get() on i equals j.Id
                                                                            select j), new IdComparer<TDomain>()).ToList();
            return requiredMongoCollection;
        }

        public IEnumerable<TDomain> GetRequiredMongoCollection(Expression<Func<TDomain, bool>> filter)
        {
            if (filter.Body.ToString().Contains("PlatformTypes"))
            {
                filter = x => false;
            }

            var sqlIds = SqlDataRepository.Get(filter).Select(sql => sql.Id);
            var requiredMongoCollection = MongoDataRepository.Get().Except((from i in sqlIds
                                                                            join j in MongoDataRepository.Get() on i equals j.Id
                                                                            select j), new IdComparer<TDomain>()).AsQueryable().Where(filter).ToList();
            return requiredMongoCollection;
        }

        public IEnumerable<TDomain> GetItems(IEnumerable<string> ids)
        {
            IList<TDomain> items = new List<TDomain>();
            foreach (var id in ids)
            {
                var item = GetItemById(id);
                if (item != null)
                {
                    items.Add(item);
                }
            }
            return items;
        }
    }
}
