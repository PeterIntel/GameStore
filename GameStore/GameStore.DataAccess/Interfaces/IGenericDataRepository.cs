using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using GameStore.Domain.BusinessObjects;

namespace GameStore.DataAccess.Interfaces
{
    public interface IGenericDataRepository<TEntity,TDomain> where TEntity : class where TDomain : BasicDomain
    {
        IEnumerable<TDomain> Get(Expression<Func<TDomain, bool>> filter, params Expression<Func<TDomain, object>>[] includeProperties);
        IEnumerable<TDomain> Get(params Expression<Func<TDomain, object>>[] includeProperties);
        TDomain GetItemById(string id);
        TDomain GetFirst(Expression<Func<TDomain, bool>> filter);
        int GetCountObject(Expression<Func<TDomain, bool>> filter);
        void Add(TDomain item);
        void Update(TDomain item);
        void Remove(TDomain item);
        void Remove(string id);
    }
}
