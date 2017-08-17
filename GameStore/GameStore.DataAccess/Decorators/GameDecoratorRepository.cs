using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GameStore.DataAccess.Interfaces;
using GameStore.DataAccess.Mongo.MongoEntities;
using GameStore.DataAccess.MSSQL.Entities;
using GameStore.Domain.BusinessObjects;

namespace GameStore.DataAccess.Decorators
{
    public class GameDecoratorRepository : GenericDecoratorRepository<GameEntity, MongoProductEntity, Game>, IGameDecoratorRepositoryRepository
    {
        public GameDecoratorRepository(IGenericDataRepository<GameEntity, Game> sqlDataRepository, IReadOnlyGenericRepository<MongoProductEntity, Game> mongoDataRepository) : base(sqlDataRepository, mongoDataRepository)
        {

        }

        public IEnumerable<Game> Get<TKey>(Expression<Func<Game, bool>> filter, Expression<Func<Game, TKey>> sort, bool ascending = true, int page = 1, int? size = 10, params Expression<Func<Game, object>>[] includeProperties)
        {
            var filteredGames = Get(filter, includeProperties);
            filteredGames = ascending ? filteredGames.AsQueryable().OrderBy(sort) : filteredGames.AsQueryable().OrderByDescending(sort);

            filteredGames = filteredGames.Skip((page - 1) * (int)size).Take((int)size);

            return filteredGames;
        }
    }
}
