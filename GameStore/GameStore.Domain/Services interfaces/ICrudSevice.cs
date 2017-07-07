using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Services_interfaces
{
    public interface ICrudService<T> where T:class
    {
        void Add(T item);
        void Update(T item);
        void Remove(T item);
        void Remove(int id);
    }
}
