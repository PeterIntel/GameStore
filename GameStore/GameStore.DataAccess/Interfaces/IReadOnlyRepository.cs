using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DataAccess.Mongo.MongoRepositories
{
    public interface IReadOnlyGenericRepository<TEntity, TDomain> where TEntity : class where TDomain : class
    {
        IEnumerable<TDomain> Get(Expression<Func<TDomain, bool>> filter);
        IEnumerable<TDomain> Get();
        TDomain GetItemById(string id);
        int GetCountObject(Expression<Func<TDomain, bool>> filter);
    }
}
