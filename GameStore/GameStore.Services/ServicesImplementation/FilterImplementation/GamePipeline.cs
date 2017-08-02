using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GameStore.Domain.BusinessObjects;

namespace GameStore.Services.ServicesImplementation.FilterImplementation
{
    public class GamePipeline : Pipeline<Game>
    {
        public override Expression<Func<Game, bool>> Process(Expression<Func<Game, bool>> input)
        {
            foreach (var filter in filters)
            {
                input = filter.Execute(input);
            }

            return input;
        }

        public Expression<Func<Game, bool>> ApplyFilters(FilterCriteria filters)
        {

            Register(new GenreFilter(filters.NameGenres));

            Register(new PlatformTypeFilter(filters.NamePlatformTypes));

            Register(new PublisherFilter(filters.NamePublishers));

            Register(new PriceFromFilter(filters.PriceFrom));

            Register(new PriceToFilter(filters.PriceTo));

            Register(new PublishedDateFilter(filters.DateTimeIntervals));

            Register(new GameNameFilter(filters.GameName));

            return Process(x => true);
        }
    }
}
