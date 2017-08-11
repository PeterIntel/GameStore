using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Domain.BusinessObjects;
using AutoMapper;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Security.Cryptography.X509Certificates;
using AutoMapper.QueryableExtensions;
using GameStore.DataAccess.Interfaces;
using GameStore.DataAccess.MSSQL.Entities;
using GameStore.DataAccess.Infrastructure;

namespace GameStore.DataAccess.MSSQL.Repositories
{
    public class GameRepository : GenericDataRepository<GameEntity, Game>
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IPlatformTypeRepository _platformRepository;
        private readonly IGenericDataRepository<PublisherEntity, Publisher> _publisherRepository;
        private readonly IGenericDataRepository<GameInfoEntity, GameInfo> _gameInfoRepository;
        public GameRepository(GamesSqlContext context, IMapper mapper, IGenreRepository genreRepository, IPlatformTypeRepository platformRepository, IGenericDataRepository<PublisherEntity, Publisher> publisherRepository, IGenericDataRepository<GameInfoEntity, GameInfo> gameInfoRepository) : base(context, mapper)
        {
            _genreRepository = genreRepository;
            _platformRepository = platformRepository;
            _publisherRepository = publisherRepository;
            _gameInfoRepository = gameInfoRepository;
        }

        public override void Add(Game game)
        {
            if (game != null)
            {
                var gameEntity = InitGame(game);
                gameEntity.IsSqlEntity = true;
                gameEntity.GameInfo = new GameInfoEntity() { IsSqlEntity = true, UploadDate = DateTime.UtcNow, CountOfViews = 0};
                _dbSet.Add(gameEntity);
            }
        }

        private GameEntity InitGame(Game game)
        {
            var gameEntity = _mapper.Map<Game, GameEntity>(game);

            if (game.Genres != null)
            {
                IEnumerable<GenreEntity> sqlGenres = _genreRepository.GetGenres(game.Genres);
                IEnumerable<Genre> notSqlGenres = game.Genres.Except(_mapper.Map<IEnumerable<GenreEntity>, IEnumerable<Genre>>(sqlGenres), new IdComparer<Genre>());
                foreach (var genre in notSqlGenres)
                {
                    _genreRepository.Add(genre);
                }
                _context.SaveChanges();

                gameEntity.Genres = _genreRepository.GetGenres(game.Genres).ToList();
            }
            if (game.PlatformTypes != null)
            {
                gameEntity.PlatformTypes = _platformRepository
                    .GetPlatformTypes(game.PlatformTypes).ToList();
            }
            if (game.Publisher.Id != null)
            {
                gameEntity.Publisher = _context.Publishers.FirstOrDefault(x => x.CompanyName == game.Publisher.CompanyName);

                if (gameEntity.Publisher == null)
                {
                    _publisherRepository.Add(game.Publisher);
                    _context.SaveChanges();
                    gameEntity.Publisher = _context.Publishers.FirstOrDefault(x => x.CompanyName == game.Publisher.CompanyName);
                }
            }

            return gameEntity;
        }
    }
}
