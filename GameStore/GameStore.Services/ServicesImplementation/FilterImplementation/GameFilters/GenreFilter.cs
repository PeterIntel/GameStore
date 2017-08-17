using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GameStore.Domain.BusinessObjects;

namespace GameStore.Services.ServicesImplementation.FilterImplementation.GameFilters
{
    public class GenreFilter : BaseFilter<Game>
    {
        private readonly IEnumerable<string> _genres;

        public GenreFilter(IEnumerable<string> genres)
        {
            _genres = genres;
        }
        public override Expression<Func<Game, bool>> Execute(Expression<Func<Game, bool>> input)
        {
            if (_genres != null && _genres.Count() != 0)
            {
                Expression<Func<Game, bool>> filter = x => x.Genres.Any(y => _genres.Contains(y.Id));
                return AggregateExpression(input, filter);
            }

            return input;
        }
    }
}
