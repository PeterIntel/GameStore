using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DataAccess.Repositories
{
    public interface IGenericDataRepository<TEntity,TDomain> where TEntity : class where TDomain : class
    {
        IEnumerable<TDomain> GetAll(Expression<Func<TDomain, bool>> filter, string includeProperties = "");
        TDomain GetItemById(int id);
        void Add(TDomain item);
        void Update(TDomain item);
        void Remove(TDomain item);
        void Remove(int id);
    }
}
