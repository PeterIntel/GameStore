using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.ServicesInterfaces
{
    public interface ICrudService<T> where T:class
    {
        IEnumerable<T> Get(params Expression<Func<T, object>>[] includeProperties);
        void Add(T item);
        void Update(T item);
        void Remove(T item);
        void Remove(string id);
    }
}
