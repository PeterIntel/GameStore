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
using GameStore.DataAccess.Interfaces;
using GameStore.DataAccess.MSSQL.Entities;

namespace GameStore.DataAccess.MSSQL.Repositories
{
    public class GameRepository : GenericDataRepository<GameEntity, Game>
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IPlatformTypeRepository _platformRepository;
        private readonly IGenericDataRepository<PublisherEntity, Publisher> _publisherRepository;
        public GameRepository(GamesSqlContext context, IMapper mapper, IGenreRepository genreRepository, IPlatformTypeRepository platformRepository, IGenericDataRepository<PublisherEntity, Publisher> publisherRepository) : base(context, mapper)
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
                gameEntity.Id = Guid.NewGuid().ToString();
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
    }
}
