using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GameStore.Domain.BusinessObjects;

namespace GameStore.Services.ServicesImplementation.FilterImplementation.GameFilters
{
    class PlatformTypeFilter : BaseFilter<Game>
    {
        private readonly IEnumerable<string> _platforms;

        public PlatformTypeFilter(IEnumerable<string> platforms)
        {
            _platforms = platforms;
        }
        public override Expression<Func<Game, bool>> Execute(Expression<Func<Game, bool>> input)
        {
            if (_platforms != null && _platforms.Count() != 0)
            {
                Expression<Func<Game, bool>> filter = x => x.PlatformTypes.Any(y => _platforms.Contains(y.Id));
                return AggregateExpression(input, filter);
            }

            return input;
        }
    }
}
