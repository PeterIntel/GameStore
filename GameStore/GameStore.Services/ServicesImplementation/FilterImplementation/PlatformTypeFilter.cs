﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GameStore.Domain.BusinessObjects;

namespace GameStore.Services.ServicesImplementation.FilterImplementation
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
            Expression<Func<Game, bool>> filter = x => x.PlatformTypes.Any(y => _platforms.Contains(y.TypeName));
            return AggregateExpression(input, filter);
        }
    }
}
