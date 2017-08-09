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
using GameStore.Services.ServicesImplementation.FilterImplementation;

namespace GameStore.Services.ServicesImplementation
{
    public class GameService : IGameService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGameDecoratorRepositoryRepository _gameRepository;
        private readonly IGenericDecoratorRepository<GameInfoEntity, MongoProductEntity, GameInfo> _gameInfoRepository;
        private GamePipeline _gamePipeline;
        public GameService(IUnitOfWork unitOfWork, IGameDecoratorRepositoryRepository gameRepository, IGenericDecoratorRepository<GameInfoEntity, MongoProductEntity, GameInfo> gameInfoRepository)
        {
            _unitOfWork = unitOfWork;
            _gameRepository = gameRepository;
            _gameInfoRepository = gameInfoRepository;
        }
        public void Add(Game item)
        {
            _gameRepository.Add(item);
            _unitOfWork.Save();
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
                    games = _gameRepository.Get(filterExpression, x => x.Price, page, maxSize);
                    break;
                case SortCriteria.ByPriceDesc:
                    games = _gameRepository.Get(filterExpression, x => x.Price * (-1), page, maxSize);
                    break;
                case SortCriteria.MostCommented:
                    games = _gameRepository.Get(filterExpression, x => x.Comments.Count() * (-1), page, maxSize);
                    break;
                case SortCriteria.New:
                    games = _gameRepository.Get(filterExpression, x => x.GameInfo.UploadDate, page, maxSize);
                    break;
                case SortCriteria.MostPopular:
                    games = _gameRepository.Get(filterExpression, x => x.GameInfo.CountOfViews * (-1), page, maxSize);
                    break;
                default:
                    games = _gameRepository.Get(filterExpression, x => x.Id, page, maxSize);
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
        }

        public void Update(Game item)
        {
            _gameRepository.Update(item);
            _unitOfWork.Save();
        }

        public void AddViewToGame(string key)
        {
            var game = _gameRepository.GetFirst(x => x.Key == key);
            var gameInfo = _gameInfoRepository.GetItemById(game.Id);
            gameInfo.CountOfViews++;
            gameInfo.Game = null;
            _gameInfoRepository.Update(gameInfo);
            _unitOfWork.Save();
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
