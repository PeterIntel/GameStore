using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using GameStore.Domain.BusinessObjects;

namespace GameStore.DataAccess.Interfaces
{
    public interface IReadOnlyGameRepository
    {
        IEnumerable<Game> Get<TKey>(Expression<Func<Game, bool>> filter, Expression<Func<Game, TKey>> sort, bool ascending = true, int page = 1, int? size = 10);
    }
}
