using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.ServicesInterfaces.FilterInterfaces
{
    public abstract class Pipeline<T>
    {
        protected readonly IList<IFilter<T>> filters = new List<IFilter<T>>();

        public Pipeline<T> Register(IFilter<T> filter)
        {
            filters.Add(filter);
            return this;
        }

        public abstract T Process(T input);
    }
}
