using GameStore.Domain.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Services.ServicesImplementation.FilterImplementation
{
    public class PriceToFilter : BaseFilter<Game>
    {
        private readonly decimal? _priceTo;

        public PriceToFilter(decimal? priceTo)
        {
            _priceTo = priceTo;
        }
        public override Expression<Func<Game, bool>> Execute(Expression<Func<Game, bool>> input)
        {
            Expression<Func<Game, bool>> filter = x => x.Price <= _priceTo;
            return AggregateExpression(input, filter);
        }
    }
}
