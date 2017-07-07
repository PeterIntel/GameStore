using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.DataAccess.Entities;
using GameStore.Domain.BusinessObjects;
using AutoMapper;

namespace GameStore.DataAccess.Repositories
{
    public class GameRepository : GenericDataRepository<GameEntity, Game>, IGameRepository
    {
        public GameRepository(GamesContext context, IMapper mapper) : base(context, mapper) { }
        public Game GetGameByKey(string key)
        {
            GameEntity entity;
            try
            {
                var listGameEntities = _dbSet.Where(x => x.Key == key && x.IsDeleted == false);
                entity = listGameEntities.First();
            }
            catch(Exception ex)
            {
                throw new Exception($"Game with the key {key} wasn't found!", ex);
            }
            
            Game domain = _mapper.Map<GameEntity, Game>(entity);
            return domain;
        }
    }
}
