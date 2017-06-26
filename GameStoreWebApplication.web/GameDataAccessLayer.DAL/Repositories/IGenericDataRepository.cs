using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GameDataAccessLayer.DAL.Repositories
{
    public interface IGenericDataRepository<T> where T : class
    {
        IList<T> GetAll(Expression<Func<T, bool>> filter, string includeProperties = "");
        T GetItemById(int id);
        void Add(T item);
        void Update(T item);
        void Remove(T item);
        void Remove(int id);
    }
}
