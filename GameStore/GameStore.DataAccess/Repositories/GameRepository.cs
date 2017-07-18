using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.DataAccess.Entities;
using GameStore.Domain.BusinessObjects;
using AutoMapper;
using GameStore.DataAccess.Context;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Security.Cryptography.X509Certificates;

namespace GameStore.DataAccess.Repositories
{
    public class GameRepository : GenericDataRepository<GameEntity, Game>, IGameRepository
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IPlatformTypeRepository _platformRepository;
        public GameRepository(GamesContext context, IMapper mapper, IGenreRepository genreRepository, IPlatformTypeRepository platformRepository) : base(context, mapper)
        {
            _genreRepository = genreRepository;
            _platformRepository = platformRepository;
        }

        public override void Add(Game game)
        {
            if (game != null)
            {
                var gameEntity = _mapper.Map<Game, GameEntity>(game);
                var genres = _mapper.Map<IEnumerable<Genre>, List<string>>(game.Genres);
                var platforms = _mapper.Map<IEnumerable<PlatformType>, List<string>>(game.PlatformTypes);
                gameEntity.Genres = _genreRepository.GetGenres(genres).ToList();
                gameEntity.PlatformTypes = _platformRepository.GetPlatformTypes(platforms).ToList();
                _dbSet.Add(gameEntity);
            }
        }

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
