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
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace GameStore.DataAccess.Decorators
{
    public class GenericDecoratorRepositoryRepository<TSqlEntity, TMongoEntity, TDomain> : IGenericDecoratorRepository<TSqlEntity, TMongoEntity, TDomain> where TSqlEntity : class where TMongoEntity : class where TDomain : BasicDomain
    {
        private readonly IGenericDataRepository<TSqlEntity, TDomain> _sqlDataRepository;
        private readonly IReadOnlyGenericRepository<TMongoEntity, TDomain> _mongoDataRepository;

        public GenericDecoratorRepositoryRepository(IGenericDataRepository<TSqlEntity, TDomain> sqlDataRepository, IReadOnlyGenericRepository<TMongoEntity, TDomain> mongoDataRepository)
        {
            _sqlDataRepository = sqlDataRepository;
            _mongoDataRepository = mongoDataRepository;
        }
        public void Add(TDomain item)
        {
            _sqlDataRepository.Add(item);
        }

        public IEnumerable<TDomain> Get(params Expression<Func<TDomain, object>>[] includeProperties)
        {
            var result = _sqlDataRepository.Get(includeProperties).Union(GetRequiredMongoCollection());
            return result;
        }

        public IEnumerable<TDomain> Get(Expression<Func<TDomain, bool>> filter, params Expression<Func<TDomain, object>>[] includeProperties)
        {
            var result = _sqlDataRepository.Get(filter, includeProperties).Union(GetRequiredMongoCollection(filter));
            //var d = result.ToList();
            return result;
        }

        public int GetCountObject(Expression<Func<TDomain, bool>> filter)
        {
            return _sqlDataRepository.GetCountObject(filter) + GetRequiredMongoCollection(filter).Count();
        }

        public TDomain GetFirst(Expression<Func<TDomain, bool>> filter)
        {
            var domain = _sqlDataRepository.GetFirst(filter) ?? _mongoDataRepository.GetFirst(filter);

            return domain;
        }

        public TDomain GetItemById(string id)
        {
            var domain = _sqlDataRepository.GetItemById(id) ?? _mongoDataRepository.GetItemById(id);

            return domain;
        }

        public void Remove(string id)
        {
            _sqlDataRepository.Remove(id);
        }

        public void Remove(TDomain item)
        {
            _sqlDataRepository.Remove(item);
        }

        public void Update(TDomain item)
        {
            _sqlDataRepository.Remove(item);
        }

        public IEnumerable<TDomain> GetRequiredMongoCollection()
        {
            var sqlIds = _sqlDataRepository.Get(x => x.IsMongoEntity).Select(sql => sql.Id);
            var requiredMongoCollection = _mongoDataRepository.Get().Except(from i in sqlIds
                                                                            join j in _mongoDataRepository.Get() on i equals j.Id
                                                                            select j);
            return requiredMongoCollection;
        }

        public IEnumerable<TDomain> GetRequiredMongoCollection(Expression<Func<TDomain, bool>> filter) 
        {
            var sqlIds = _sqlDataRepository.Get(filter).Where(x => x.IsMongoEntity).Select(sql => sql.Id);
            var requiredMongoCollection = _mongoDataRepository.Get().Except(from i in sqlIds
                                                                            join j in _mongoDataRepository.Get() on i equals j.Id
                                                                            select j).AsQueryable().Where(filter);
            return requiredMongoCollection;
        }
    }
}
