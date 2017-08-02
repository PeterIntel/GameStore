using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GameStore.DataAccess.Entities;
using GameStore.Domain.BusinessObjects;

namespace GameStore.DataAccess.Repositories
{
    public interface IGameRepository : IGenericDataRepository<GameEntity, Game>
    {
        IEnumerable<Game> Get<TKey>(Expression<Func<Game, bool>> filter, Expression<Func<Game, TKey>> sort, int page = 1, int? size = 10, params Expression<Func<Game, object>>[] includeProperties);
        Game GetGameByKey(string key);
    }
}
