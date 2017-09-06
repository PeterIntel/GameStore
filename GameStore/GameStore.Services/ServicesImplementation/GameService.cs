using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GameStore.DataAccess.Interfaces;
using GameStore.DataAccess.MSSQL.Entities;
using GameStore.DataAccess.UnitOfWork;
using GameStore.Domain.BusinessObjects;
using GameStore.Domain.BusinessObjects.LocalizationObjects;
using GameStore.Domain.ServicesInterfaces;
using GameStore.Logging.Loggers;
using GameStore.Services.Localization;
using GameStore.Services.ServicesImplementation.FilterImplementation.GameFilters;

namespace GameStore.Services.ServicesImplementation
{
    public class GameService : BasicService<GameEntity, Game>, IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IGenericDataRepository<GameInfoEntity, GameInfo> _gameInfoRepository;
        private readonly IGenericDataRepository<GenreEntity, Genre> _genreRepository;
        private readonly IGenericDataRepository<PlatformTypeEntity, PlatformType> _platformRepository;
        private readonly IGenericDataRepository<PublisherEntity, Publisher> _publisherRepository;
        private GamePipeline _gamePipeline;

        public GameService(IUnitOfWork unitOfWork, IGameRepository gameRepository, IGenericDataRepository<GameInfoEntity, GameInfo> gameInfoRepository, IGenericDataRepository<GenreEntity, Genre> genreRepository, IGenericDataRepository<PublisherEntity, Publisher> publisherRepository, IMongoLogger<Game> logger,
            ILocalizationProvider<Game> localizatorProvider, IGenericDataRepository<PlatformTypeEntity, PlatformType> platformRepository) :
            base(gameRepository, unitOfWork, logger, localizatorProvider)
        {
            _gameRepository = gameRepository;
            _gameInfoRepository = gameInfoRepository;
            _genreRepository = genreRepository;
            _publisherRepository = publisherRepository;
            _platformRepository = platformRepository;
        }
        public override void Add(Game item, string cultureCode)
        {
            AssignIdIfEmpty(item);
            if (item.Genres == null)
            {
                item.Genres = item.NameGenres != null ? _genreRepository.LoadDomainEntities(item.NameGenres) : _genreRepository.Get(genre => genre.Name == "Other").ToList();
            }

            item.PlatformTypes = item.NamePlatformTypes != null ? _platformRepository.LoadDomainEntities(item.NamePlatformTypes) : null;

            if (item.Publisher != null)
            {
                item.Publisher = _publisherRepository.GetItemById(item.Publisher.Id);
            }

            if (!item.Locals.Any())
            {
                item.Locals = new List<GameLocal>()
                {
                    new GameLocal()
                    {
                        Culture = new Culture() {Code = cultureCode},
                        Description = item.Description
                    }
                };
            }

            _gameRepository.Add(item);
            UnitOfWork.Save();
            Logger.Write(Operation.Insert, item);
        }

        public override void Update(Game game, string cultureCode)
        {
            if (game.Genres == null)
            {
                game.Genres = game.NameGenres != null ? _genreRepository.LoadDomainEntities(game.NameGenres) : _genreRepository.Get(genre => genre.Name == "Other").ToList();
            }

            game.PlatformTypes = game.NamePlatformTypes != null ? _platformRepository.LoadDomainEntities(game.NamePlatformTypes) : null;

            if (game.Publisher != null)
            {
                game.Publisher = _publisherRepository.GetItemById(game.Publisher.Id);
            }

            game.Locals = new List<GameLocal>()
            {
                  new GameLocal()
                  {
                      Culture = new Culture() {Code = cultureCode},
                      Description = game.Description
                  }
            };

            base.Update(game, cultureCode);
        }

        public Game GetItemByKey(string key, string cultureCode)
        {
            var game = _gameRepository.First(x => x.Key == key);
            LocalizationProvider.Localize(game, cultureCode);

            return game;
        }

        public PaginationGames FilterGames(FilterCriteria filters, int page, string size, string cultureCode)
        {
            _gamePipeline = new GamePipeline();
            var filterExpression = _gamePipeline.ApplyFilters(filters);

            IEnumerable<Game> games;
            int? maxSize = size != "ALL" ? (int?)int.Parse(size) : null;

            switch (filters.SortCriteria)
            {
                case SortCriteria.ByPriceAsc:
                    games = _gameRepository.Get(filterExpression, x => x.Price, true, page, maxSize);
                    break;
                case SortCriteria.ByPriceDesc:
                    games = _gameRepository.Get(filterExpression, x => x.Price, false, page, maxSize);
                    break;
                case SortCriteria.MostCommented:
                    games = _gameRepository.Get(filterExpression, x => x.Comments.Count(), false, page, maxSize);
                    break;
                case SortCriteria.New:
                    games = _gameRepository.Get(filterExpression, x => x.GameInfo.UploadDate, false, page, maxSize);
                    break;
                case SortCriteria.MostPopular:
                    games = _gameRepository.Get(filterExpression, x => x.GameInfo.CountOfViews, false, page, maxSize);
                    break;
                default:
                    games = _gameRepository.Get(filterExpression, x => x.Id, true, page, maxSize);
                    break;
            }

            var filteredGames = new PaginationGames()
            {
                Count = _gameRepository.GetCountObject(filterExpression),
                Games = games.ToList()
            };

            foreach (var game in filteredGames.Games)
            {
                LocalizationProvider.Localize(game, cultureCode);
            }

            return filteredGames;
        }

        public void AddViewToGame(string key, string cultureCode)
        {
            var game = _gameRepository.First(x => x.Key == key);
            if (game != null)
            {
                if (game.IsSqlEntity == false)
                {
                    Add(game, cultureCode);
                    game = _gameRepository.First(x => x.Key == key);
                }
                var gameInfo = _gameInfoRepository.GetItemById(game.Id);
                gameInfo.CountOfViews++;
                gameInfo.Game = null;
                _gameInfoRepository.Update(gameInfo);
                UnitOfWork.Save();
                var updatedGame = _gameRepository.GetItemById(game.Id);
                Logger.Write(Operation.Update, game, updatedGame);
            }
        }

        public new PaginationGames Get(string cultureCode, params Expression<Func<Game, object>>[] includeProperties)
        {
            var games = new PaginationGames()
            {
                Count = _gameRepository.GetCountObject(x => true),
                Games = _gameRepository.Get(x => true, x => x.Id).ToList()
            };

            foreach (var game in games.Games)
            {
                LocalizationProvider.Localize(game, cultureCode);
            }

            return games;
        }
    }
}
