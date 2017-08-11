using GameStore.Domain.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GameStore.DataAccess.Mongo.MongoEntities;
using GameStore.DataAccess.MSSQL.Entities;

namespace GameStore.DataAccess.Decorators
{
    public interface IGameDecoratorRepositoryRepository : IGenericDecoratorRepository<GameEntity, MongoProductEntity, Game>
    {
        IEnumerable<Game> Get<TKey>(Expression<Func<Game, bool>> filter, Expression<Func<Game, TKey>> sort, bool ascending = true, int page = 1, int? size = 10, params Expression<Func<Game, object>>[] includeProperties);
    }
}
