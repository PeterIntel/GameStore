using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GameStore.Domain.ServicesInterfaces
{
    public interface ICrudService<T> where T:class
    {
        IEnumerable<T> Get(string cultureCode, params Expression<Func<T, object>>[] includeProperties);
        IEnumerable<T> Get(Expression<Func<T, bool>> filter, string cultureCode, params Expression<Func<T, object>>[] includeProperties);
        void Add(T item, string cultureCode);
        void Update(T game, string cultureCode);
        void Remove(T item);
        void Remove(string id);
        T First(Expression<Func<T, bool>> filter, string cultureCode);
        bool Any(Expression<Func<T, bool>> filter);
        T GetItemById(string id);
    }
}
