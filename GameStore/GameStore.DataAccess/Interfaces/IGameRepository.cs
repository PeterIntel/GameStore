using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using GameStore.DataAccess.MSSQL.Entities;
using GameStore.Domain.BusinessObjects;

namespace GameStore.DataAccess.Interfaces
{
    public interface IGameRepository : IGenericDataRepository<GameEntity, Game>
    {
        IEnumerable<Game> Get<TKey>(Expression<Func<Game, bool>> filter, Expression<Func<Game, TKey>> sort, bool ascending = true, int page = 1, int? size = 10, params Expression<Func<Game, object>>[] includeProperties);
    }
}
