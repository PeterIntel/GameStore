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
    public class GameDecoratorRepositoryRepository : GenericDecoratorRepositoryRepository<GameEntity, MongoProductEntity, Game>, IGameDecoratorRepositoryRepository
    {
        private readonly IGenericDataRepository<GameEntity, Game> _sqlGameRepository;
        private readonly IReadOnlyGenericRepository<MongoProductEntity, Game> _mongoGameRepository;
        public GameDecoratorRepositoryRepository(IGenericDataRepository<GameEntity, Game> sqlDataRepository, IReadOnlyGenericRepository<MongoProductEntity, Game> mongoDataRepository) : base(sqlDataRepository, mongoDataRepository)
        {
            _sqlGameRepository = sqlDataRepository;
            _mongoGameRepository = mongoDataRepository;
        }

        public IEnumerable<Game> Get<TKey>(Expression<Func<Game, bool>> filter, Expression<Func<Game, TKey>> sort, int page = 1, int? size = 10, params Expression<Func<Game, object>>[] includeProperties)
        {
            var filteredGames = Get(filter, includeProperties);
            filteredGames = filteredGames.AsQueryable().OrderBy(sort);
            if (size != null)
            {
                filteredGames = filteredGames.Skip((page - 1) * (int)size).Take((int)size);
            }

            return filteredGames;
        }
    }
}
