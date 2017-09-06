using System;
using System.Linq.Expressions;
using GameStore.Domain.BusinessObjects;

namespace GameStore.Services.ServicesImplementation.FilterImplementation.GameFilters
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

            if (_priceFrom != null)
            {
                Expression<Func<Game, bool>> filter = x => x.Price >= _priceFrom;
                return AggregateExpression(input, filter);
            }

            return input;
        }
    }
}
