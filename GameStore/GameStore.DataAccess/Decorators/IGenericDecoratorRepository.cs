using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GameStore.DataAccess.Interfaces;
using GameStore.Domain.BusinessObjects;

namespace GameStore.DataAccess.Decorators
{
    public interface IGenericDecoratorRepository<TSqlEntity, TMongoEntity, TDomain> : IGenericDataRepository<TSqlEntity, TDomain> where TSqlEntity : class where TMongoEntity : class where TDomain : BasicDomain
    {
        IEnumerable<TDomain> GetRequiredMongoCollection();
        IEnumerable<TDomain> GetRequiredMongoCollection(Expression<Func<TDomain, bool>> filter);
    }
}
