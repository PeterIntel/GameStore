using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using GameStore.Domain.BusinessObjects;

namespace GameStore.DataAccess.Interfaces
{
    public interface IReadOnlyGenericRepository<TEntity, TDomain> where TEntity : class where TDomain : BasicDomain
    {
        IEnumerable<TDomain> Get(Expression<Func<TDomain, bool>> filter);
        IEnumerable<TDomain> Get();
        TDomain GetItemById(string id);
        TDomain First(Expression<Func<TDomain, bool>> filter);
        int GetCountObject(Expression<Func<TDomain, bool>> filter);
    }
}
