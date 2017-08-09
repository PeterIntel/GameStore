using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using GameStore.Domain.BusinessObjects;

namespace GameStore.DataAccess.Interfaces
{
    public interface IReadOnlyGenericRepository<TEntity, TDomain> where TEntity : class where TDomain : BasicDomain
    {
        IEnumerable<TDomain> Get(Expression<Func<TDomain, bool>> filter, params string[] ids);
        IEnumerable<TDomain> Get(params string[] ids);
        TDomain GetItemById(string id);
        TDomain GetFirst(Expression<Func<TDomain, bool>> filter);
        int GetCountObject(Expression<Func<TDomain, bool>> filter);
    }
}
