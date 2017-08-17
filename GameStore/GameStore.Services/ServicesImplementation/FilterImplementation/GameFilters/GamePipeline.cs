using System;
using System.Linq.Expressions;
using GameStore.Domain.BusinessObjects;

namespace GameStore.Services.ServicesImplementation.FilterImplementation.GameFilters
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
