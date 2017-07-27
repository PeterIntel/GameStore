﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GameStore.Domain.BusinessObjects;

namespace GameStore.Services.ServicesImplementation.FilterImplementation
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
            Expression<Func<Game, bool>> filter = x => x.Genres.Any(y => _genres.Contains(y.Name));
            return AggregateExpression(input, filter);
        }
    }
}
