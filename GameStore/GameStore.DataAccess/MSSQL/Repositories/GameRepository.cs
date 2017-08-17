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
    public class GameRepository : GenericDataRepository<GameEntity, Game>, IGameRepository
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
                gameEntity.PlatformTypes = _platformRepository.GetPlatformTypes(game.PlatformTypes).ToList();
            }
            if (game.Publisher != null)
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

        public IEnumerable<Game> Get<TKey>(Expression<Func<Game, bool>> filterDomain, Expression<Func<Game, TKey>> sortDomain, bool ascending = true, int page = 1, int? size = 10, params Expression<Func<Game, object>>[] includeProperties)
        {
            IQueryable<GameEntity> queryToEntity = _dbSet.Where(x => x.IsDeleted == false);

            var filterEntity = _mapper.Map<Expression<Func<Game, bool>>, Expression<Func<GameEntity, bool>>>(filterDomain);

            var sortEntity = _mapper.Map<Expression<Func<Game, TKey>>, Expression<Func<GameEntity, TKey>>>(sortDomain);

            var includePropertiesForEntities = _mapper
                .Map<Expression<Func<Game, object>>[], Expression<Func<GameEntity, object>>[]>(includeProperties);

            foreach (var item in includePropertiesForEntities)
            {
                queryToEntity.Include(item);
            }

            if (filterEntity != null)
            {
                queryToEntity = queryToEntity.Where(filterEntity);
            }

            queryToEntity = ascending ? queryToEntity.OrderBy(sortEntity) : queryToEntity.OrderByDescending(sortEntity);

            queryToEntity = queryToEntity.Take((int)size * page);

            var result = queryToEntity.ProjectTo<Game>(_mapper.ConfigurationProvider);
            return result;
        }
    }
}
