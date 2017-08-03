using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Services.ServicesImplementation.FilterImplementation
{
    public abstract class Pipeline<TSource>
    {
        protected readonly IList<BaseFilter<TSource>> filters = new List<BaseFilter<TSource>>();

        public void Register(BaseFilter<TSource> baseFilter)
        {
            filters.Add(baseFilter);
        }

        public abstract Expression<Func<TSource, bool>> Process(Expression<Func<TSource, bool>> input);
    }
}
