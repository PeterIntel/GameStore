﻿using System;
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
        public GameDecoratorRepositoryRepository(IGenericDataRepository<GameEntity, Game> sqlDataRepository, IReadOnlyGenericRepository<MongoProductEntity, Game> mongoDataRepository) : base(sqlDataRepository, mongoDataRepository)
        {

        }

        // TODO: Skip and Take actions should also be executed in the database
        public IEnumerable<Game> Get<TKey>(Expression<Func<Game, bool>> filter, Expression<Func<Game, TKey>> sort, bool ascending = true, int page = 1, int? size = 10, params Expression<Func<Game, object>>[] includeProperties)
        {
            var filteredGames = Get(filter, includeProperties);
            // TODO: You don't need IQueriable here, connection to the db doesn't exists here anymore.
            filteredGames = ascending ? filteredGames.AsQueryable().OrderBy(sort) : filteredGames.AsQueryable().OrderByDescending(sort);
            if (size != null)
            {
                filteredGames = filteredGames.Skip((page - 1) * (int)size).Take((int)size);
            }

            return filteredGames;
        }
    }
}
