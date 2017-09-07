using System;
using System.Linq.Expressions;
using GameStore.Domain.BusinessObjects;

namespace GameStore.Services.ServicesImplementation.FilterImplementation.GameFilters
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
            if (_priceTo != null)
            {
                Expression<Func<Game, bool>>  filter = x => x.Price <= _priceTo;
                return AggregateExpression(input, filter);
            }

            return input;
        }
    }
}
