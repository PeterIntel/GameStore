using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using GameStore.DataAccess.Infrastructure;
using GameStore.DataAccess.Interfaces;
using GameStore.DataAccess.MSSQL.Entities;
using GameStore.DataAccess.MSSQL.Entities.Localization;
using GameStore.Domain.BusinessObjects;

namespace GameStore.DataAccess.MSSQL.Repositories
{
    public class GameRepository : GenericDataRepository<GameEntity, Game>, IGameRepository
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IPlatformTypeRepository _platformRepository;
        private readonly IGenericDataRepository<PublisherEntity, Publisher> _publisherRepository;
        private readonly IGenericDataRepository<GameInfoEntity, GameInfo> _gameInfoRepository;
        private readonly ICultureRepository _cultureRepository;

        public GameRepository(GamesSqlContext context, IMapper mapper, IGenreRepository genreRepository, IPlatformTypeRepository platformRepository, IGenericDataRepository<PublisherEntity, Publisher> publisherRepository, IGenericDataRepository<GameInfoEntity, GameInfo> gameInfoRepository,
            ICultureRepository cultureRepository) : base(context, mapper)
        {
            _genreRepository = genreRepository;
            _platformRepository = platformRepository;
            _publisherRepository = publisherRepository;
            _gameInfoRepository = gameInfoRepository;
            _cultureRepository = cultureRepository;
        }

        public override void Add(Game game)
        {
            if (game != null)
            {
                var gameEntity = InitGame(game);
                gameEntity.IsSqlEntity = true;

                var id = GetGuidId();
                var currentCulture = game.Locals.First().Culture.Code;
                var description = game.Locals.First().Description;
                gameEntity.Locals = new List<GameLocalEntity>
                {
                    new GameLocalEntity()
                    {
                        Id = id,
                        Culture = _cultureRepository.GetCultureByCode(currentCulture),
                        Description = description
                    }
                };

                gameEntity.GameInfo = new GameInfoEntity() { IsSqlEntity = true, UploadDate = DateTime.UtcNow, CountOfViews = 0 };
                _dbSet.Add(gameEntity);
            }
        }

        private GameEntity InitGame(Game game)
        {
            var gameEntity = _mapper.Map<Game, GameEntity>(game);

            if (game.Genres != null)
            {
                IEnumerable<GenreEntity> sqlGenres = _genreRepository.GetGenres(game.Genres);
                IEnumerable<Genre> notSqlGenres = game.Genres.Except(_mapper.Map<IEnumerable<GenreEntity>, IEnumerable<Genre>>(sqlGenres), new IdDomainComparer<Genre>());
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
            var d = result.ToList();
            return result;
        }

        public override void Update(Game game)
        {
            if (game != null)
            {
                var entityGame = InitGame(game);
                var existingGame = _dbSet.Include(x => x.Genres).Include(x => x.PlatformTypes).Include(x => x.Publisher).First(x => x.Id == entityGame.Id);
                _mapper.Map(entityGame, existingGame);

                var deletedGenres = existingGame.Genres.Except(entityGame.Genres, new IdEntityComparer<GenreEntity>());
                var addedGenres = entityGame.Genres.Except(existingGame.Genres, new IdEntityComparer<GenreEntity>());
                for (int i = 0; i < deletedGenres.Count(); i++)
                {
                    existingGame.Genres.Remove(deletedGenres.ElementAt(i));
                }

                foreach (var genreEntity in addedGenres)
                {
                    existingGame.Genres.Add(genreEntity);
                }

                var deletedPlatforms = existingGame.PlatformTypes.Except(entityGame.PlatformTypes, new IdEntityComparer<PlatformTypeEntity>());
                var addedPlatforms = entityGame.PlatformTypes.Except(existingGame.PlatformTypes, new IdEntityComparer<PlatformTypeEntity>());
                for (int i = 0; i < deletedPlatforms.Count(); i++)
                {
                    existingGame.PlatformTypes.Remove(deletedPlatforms.ElementAt(i));
                }

                foreach (var platformEntity in addedPlatforms)
                {
                    existingGame.PlatformTypes.Add(platformEntity);
                }

                var currentCulture = game.Locals.First().Culture.Code;
                var description = game.Locals.First().Description;
                var local = _dbSet.Local.FirstOrDefault(x => x.Id == entityGame.Id && x.Locals.Any(y => y.Culture.Code == currentCulture));

                if (local == null)
                {
                    var id = GetGuidId();
                    existingGame.Locals.Add(

                        new GameLocalEntity()
                        {
                            Id = id,
                            Culture = _cultureRepository.GetCultureByCode(currentCulture),
                            Description = description
                        }
                    );
                }
                else
                {
                    local.Locals.First(x => x.Culture.Code == currentCulture).Description = game.Locals.First().Description;
                }

                if (_context.Entry(existingGame).State == EntityState.Detached)
                {
                    _context.Games.Attach(existingGame);
                }

                _context.Entry(existingGame).State = EntityState.Modified;
            }
        }

        public override Game First(Expression<Func<Game, bool>> filter)
        {
            var filterEntity = _mapper.Map<Expression<Func<Game, bool>>, Expression<Func<GameEntity, bool>>>(filter);
            if (filter != null)
            {
                IQueryable<GameEntity> queryToEntities = _dbSet.Where(filterEntity);
                return queryToEntities.ProjectTo<Game>(_mapper.ConfigurationProvider).FirstOrDefault();
            }

            return null;
        }

        public override Game GetItemById(string id)
        {
            GameEntity entity = _dbSet.Find(id);

            if (entity != null)
            {
                Game domain = _mapper.Map<GameEntity, Game>(entity);
                return domain;
            }

            return null;
        }
    }
}
