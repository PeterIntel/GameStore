using System;
using System.Collections.Generic;
using System.Linq;
using GameStore.DataAccess.UnitOfWork;
using GameStore.Domain.BusinessObjects;
using GameStore.Domain.ServicesInterfaces;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using GameStore.DataAccess.Decorators;
using GameStore.DataAccess.Mongo.MongoEntities;
using GameStore.DataAccess.MSSQL.Entities;
using AutoMapper;
using GameStore.DataAccess.Interfaces;
using GameStore.Logging.Loggers;
using GameStore.Services.ServicesImplementation.FilterImplementation.GameFilters;

namespace GameStore.Services.ServicesImplementation
{
    public class GameService : BasicService<Game>, IGameService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGameDecoratorRepositoryRepository _gameRepository;
        private readonly IGenericDataRepository<GameInfoEntity, GameInfo> _gameInfoRepository;
        private readonly IGenericDecoratorRepository<GenreEntity, MongoCategoryEntity, Genre> _genreRepository;
        private readonly IGenericDecoratorRepository<PublisherEntity, MongoSupplierEntity, Publisher> _publisherRepository;
        private readonly IMapper _mapper;
        private readonly IMongoLogger<Game> _logger;
        private GamePipeline _gamePipeline;
        public GameService(IUnitOfWork unitOfWork, IGameDecoratorRepositoryRepository gameRepository, IGenericDataRepository<GameInfoEntity, GameInfo> gameInfoRepository, IGenericDecoratorRepository<GenreEntity, MongoCategoryEntity, Genre> genreRepository, IGenericDecoratorRepository<PublisherEntity, MongoSupplierEntity, Publisher> publisherRepository,  IMapper mapper, IMongoLogger<Game> logger)
        {
            _unitOfWork = unitOfWork;
            _gameRepository = gameRepository;
            _gameInfoRepository = gameInfoRepository;
            _genreRepository = genreRepository;
            _publisherRepository = publisherRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public void Add(Game item)
        {
            AssignIdIfEmpty(item);
            if (item.Genres == null)
            {
                item.Genres = _genreRepository.GetItems(item.NameGenres);
            }
            item.PlatformTypes = _mapper.Map<IEnumerable<string>, IEnumerable<PlatformType>>(item.NamePlatformTypes);
            if (item.Publisher != null)
            {
                item.Publisher = _publisherRepository.GetItemById(item.Publisher.Id);
            }
            _gameRepository.Add(item);
            _unitOfWork.Save();
            _logger.Write(Operation.Insert, item);
        }

        public Game GetItemByKey(string key)
        {
            var result = _gameRepository.GetFirst(x => x.Key == key);
            return result;
        }

        public PaginationGames FilterGames(FilterCriteria filters, int page, string size)
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

            return filteredGames;
        }

        public void Remove(string id)
        {
            _gameRepository.Remove(id);
            _unitOfWork.Save();
        }

        public void Remove(Game item)
        {
            _gameRepository.Remove(item);
            _unitOfWork.Save();
            _logger.Write(Operation.Delete, item);
        }

        public void Update(Game item)
        {
            _gameRepository.Update(item);
            _unitOfWork.Save();
            var updatedGame = _gameRepository.GetItemById(item.Id);
            _logger.Write(Operation.Update, item, updatedGame);
        }

        public void AddViewToGame(string key)
        {
            var game = _gameRepository.GetFirst(x => x.Key == key);
            if (game != null)
            {
                if (game.IsSqlEntity == false)
                {
                    Add(game);
                    game = _gameRepository.GetFirst(x => x.Key == key);
                }
                var gameInfo = _gameInfoRepository.GetItemById(game.Id);
                gameInfo.CountOfViews++;
                gameInfo.Game = null;
                _gameInfoRepository.Update(gameInfo);
                _unitOfWork.Save();
                var updatedGame = _gameRepository.GetItemById(game.Id);
                _logger.Write(Operation.Update, game, updatedGame);
            }
        }

        public PaginationGames Get(params Expression<Func<Game, object>>[] includeProperties)
        {
            var games = new PaginationGames()
            {
                Count = _gameRepository.GetCountObject(x => true),
                Games = _gameRepository.Get(x => true, x => x.Id).ToList()
            };
            return games;
        }

        IEnumerable<Game> ICrudService<Game>.Get(params Expression<Func<Game, object>>[] includeProperties)
        {
            var games = _gameRepository.Get(x => true, x => x.Id).ToList();
            return games;
        }
    }
}
