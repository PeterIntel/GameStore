using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.services.Services
{
    public interface IService<T> where T:class
    {
        void Add(T item);
        void Update(T item);
        void Remove(T item);
        void Remove(int id);
    }
}
