using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Domain.BusinessObjects;
using AutoMapper;
using System.Linq.Expressions;
using System.Data.Entity;
using AutoMapper.QueryableExtensions;
using GameStore.DataAccess.MSSQL.Entities;

namespace GameStore.DataAccess.MSSQL.Repositories
{
    public class GameRepository : GenericDataRepository<GameEntity, Game>, IGameRepository
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IPlatformTypeRepository _platformRepository;
        private readonly IPublisherRepository _publisherRepository;
        public GameRepository(GamesSqlContext context, IMapper mapper, IGenreRepository genreRepository, IPlatformTypeRepository platformRepository, IPublisherRepository publisherRepository) : base(context, mapper)
        {
            _genreRepository = genreRepository;
            _platformRepository = platformRepository;
            _publisherRepository = publisherRepository;
        }

        public override void Add(Game game)
        {
            if (game != null)
            {
                var gameEntity = _mapper.Map<Game, GameEntity>(game);
                if (game.NameGenres != null)
                {
                    gameEntity.Genres = _genreRepository.GetGenres((IList<string>)game.NameGenres).ToList();
                }
                if (game.NamePlatformTypes != null)
                {
                    gameEntity.PlatformTypes = _platformRepository
                        .GetPlatformTypes((IList<string>)game.NamePlatformTypes).ToList();
                }
                if (game.Publisher.CompanyName != null)
                {
                    gameEntity.Publisher =
                        _context.Publishers.FirstOrDefault(x => x.CompanyName == game.Publisher.CompanyName);
                }

                _dbSet.Add(gameEntity);
            }
        }

        public IEnumerable<Game> Get<TKey>(Expression<Func<Game, bool>> filterDomain, Expression<Func<Game, TKey>> sortDomain, int page = 1, int? size = 10, params Expression<Func<Game, object>>[] includeProperties)
        {
            IQueryable<GameEntity> queryToEntity = _dbSet.Where(x => x.IsDeleted == false);

            var filterEntity = _mapper.Map<Expression<Func<Game, bool>>, Expression<Func<GameEntity, bool>>>(filterDomain);

            var sortEntity = _mapper.Map<Expression<Func<Game, TKey>>, Expression<Func<GameEntity, TKey>>>(sortDomain);

            var includePropertiesForEntities = _mapper.Map<Expression<Func<Game, object>>[], Expression<Func<GameEntity, object>>[]>(includeProperties);

            foreach (var item in includePropertiesForEntities)
            {
                queryToEntity.Include(item);
            }

            if (filterEntity != null)
            {
                queryToEntity = queryToEntity.Where(filterEntity);
            }

            if (sortEntity != null)
            {
                queryToEntity = queryToEntity.OrderBy(sortEntity);
            }
            else
            {
                queryToEntity = queryToEntity.OrderBy(x => x.Id);
            }

            if (size != null)
            {
                queryToEntity = queryToEntity.Skip((page - 1) * (int)size).Take((int)size);
            }

            var result = queryToEntity.ProjectTo<Game>(_mapper.ConfigurationProvider);

            return result;
        }

        public Game GetGameByKey(string key)
        {

            GameEntity entity;
            try
            {
                var listGameEntities = _dbSet.Where(x => x.Key == key && x.IsDeleted == false);
                entity = listGameEntities.First();
            }
            catch (Exception ex)
            {
                throw new Exception($"Game with the key {key} wasn't found!", ex);
            }

            Game domain = _mapper.Map<GameEntity, Game>(entity);
            return domain;
        }
    }
}
