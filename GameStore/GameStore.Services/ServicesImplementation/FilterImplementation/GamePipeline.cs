﻿using System;
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
            /// TODO: You should register all filters regardless which parameters were obtained. 
            if (filters.NameGenres != null && filters.NameGenres.Count != 0)
            {
                Register(new GenreFilter(filters.NameGenres));
            }

            if ( filters.NamePlatformTypes != null && filters.NamePlatformTypes.Count != 0)
            {
                Register(new PlatformTypeFilter(filters.NamePlatformTypes));
            }

            if (filters.NamePublishers != null && filters.NamePublishers.Count != 0)
            {
                Register(new PublisherFilter(filters.NamePublishers));
            }

            if (filters.PriceFrom != null)
            {
                Register(new PriceFromFilter(filters.PriceFrom));
            }

            if (filters.PriceTo != null)
            {
                Register(new PriceToFilter(filters.PriceTo));
            }

            if (filters.DateTimeIntervals != DateTimeIntervals.AllTime)
            {
                Register(new PublishedDateFilter(filters.DateTimeIntervals));
            }

            if (filters.GameName != null)
            {
                Register(new GameNameFilter(filters.GameName));
            }

            return Process(x => true);
        }
    }
}
