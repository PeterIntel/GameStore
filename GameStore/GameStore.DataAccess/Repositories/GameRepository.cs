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
                entity = _dbSet.Where(x => x.Key == key && x.IsDeleted == false).First();
            }
            catch(Exception ex)
            {
                throw new Exception(string.Format("Game with the key {0} wasn't found!", key), ex);
            }
            
            Game domain = _mapper.Map<GameEntity, Game>(entity);
            return domain;
        }
    }
}
