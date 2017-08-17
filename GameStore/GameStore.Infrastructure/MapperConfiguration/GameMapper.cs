using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Domain.BusinessObjects;
using GameStore.DataAccess.MSSQL.Entities;

namespace GameStore.Infrastructure.MapperConfiguration
{
    class GameMapper
    {
        public static IQueryable<Game> MapGame(IQueryable<GameEntity> entity)
        {
            return entity.Select(src => new Game()
            {

            });
        }
    }
}
