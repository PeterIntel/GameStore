using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GameStore.Domain.BusinessObjects;

namespace GameStore.DataAccess.Repositories
{
    public interface IGenericDataRepository<TEntity,TDomain> where TEntity : class where TDomain : class
    {
        IEnumerable<TDomain> Get(Expression<Func<TDomain, bool>> filter, params Expression<Func<TDomain, object>>[] includeProperty);
        IEnumerable<TDomain> Get(params Expression<Func<TDomain, object>>[] includeProperty);
        TDomain GetItemById(int id);
        int GetCountObject(Expression<Func<TDomain, bool>> filter);
        void Add(TDomain item);
        void Update(TDomain item);
        void Remove(TDomain item);
        void Remove(int id);
    }
}
