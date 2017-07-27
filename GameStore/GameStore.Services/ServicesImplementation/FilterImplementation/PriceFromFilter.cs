using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GameStore.Domain.BusinessObjects;

namespace GameStore.Services.ServicesImplementation.FilterImplementation
{
    public class PriceFromFilter : BaseFilter<Game>
    {
        private readonly decimal? _priceFrom;

        public PriceFromFilter(decimal? priceFrom)
        {
            _priceFrom = priceFrom;
        }
        public override Expression<Func<Game, bool>> Execute(Expression<Func<Game, bool>> input)
        {
            Expression<Func<Game, bool>> filter = x => x.Price >= _priceFrom;
            return AggregateExpression(input, filter);
        }
    }
}
