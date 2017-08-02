using System;
using System.Collections.Generic;
using System.Linq;
using GameStore.DataAccess.UnitOfWork;
using GameStore.Domain.BusinessObjects;
using GameStore.Domain.ServicesInterfaces;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using GameStore.Services.ServicesImplementation.FilterImplementation;

namespace GameStore.Services.ServicesImplementation
{
    public class GameService : IGameService
    {
        private readonly IUnitOfWork _unitOfWork;
        private GamePipeline _gamePipeline;
        public GameService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Add(Game item)
        {
            _unitOfWork.GameRepository.Add(item);
            _unitOfWork.Save();
        }

        public Game GetItemByKey(string key)
        {
            var result = _unitOfWork.GameRepository.GetGameByKey(key);
            return result;
        }

        // TODO: Why do you need out count? count == returnedCollection.Count
        public PaginationGames FilterGames(FilterCriteria filters, int page, string size)
        {
            _gamePipeline = new GamePipeline();
            // TODO: Why methods Apply and Process are seoarated? Use one method that receives parameters and returns expression.
            var filterExpression = _gamePipeline.ApplyFilters(filters);
            // TODO: x => true is default condition, use it inside pipeline, not outside.
            IEnumerable<Game> games;
            int? maxSize = size != "ALL" ? (int?)int.Parse(size) : null;

            switch (filters.SortCriteria)
            {
                case SortCriteria.ByPriceAsc:
                    games = _unitOfWork.GameRepository.Get(filterExpression, x => x.Price, page, maxSize);
                    break;
                case SortCriteria.ByPriceDesc:
                    games = _unitOfWork.GameRepository.Get(filterExpression, x => x.Price * (-1), page, maxSize);
                    break;
                case SortCriteria.MostCommented:
                    games = _unitOfWork.GameRepository.Get(filterExpression, x => x.Comments.Count() * (-1), page, maxSize);
                    break;
                case SortCriteria.New:
                    games = _unitOfWork.GameRepository.Get(filterExpression, x => x.GameInfo.UploadDate, page, maxSize);
                    break;
                case SortCriteria.MostPopular:
                    games = _unitOfWork.GameRepository.Get(filterExpression, x => x.GameInfo.CountOfViews * (-1), page, maxSize);
                    break;
                default:
                    games = _unitOfWork.GameRepository.Get(filterExpression, x => x.Id, page, maxSize);
                    break;
            }

            // TODO: You should call pipeline process 
            var filteredGames = new PaginationGames()
            {
                Count = _unitOfWork.GameRepository.GetCountObject(filterExpression),
                Games = games
            };

            return filteredGames;
        }

        public void Remove(int id)
        {
            _unitOfWork.GameRepository.Remove(id);
            _unitOfWork.Save();
        }

        public void Remove(Game item)
        {
            _unitOfWork.GameRepository.Remove(item);
            _unitOfWork.Save();
        }

        public void Update(Game item)
        {
            _unitOfWork.GameRepository.Update(item);
            _unitOfWork.Save();
        }

        public void AddViewToGame(string key)
        {
            var game = _unitOfWork.GameRepository.GetGameByKey(key);
            var gameInfo = _unitOfWork.GameInfoRepository.GetItemById(game.Id);
            gameInfo.CountOfViews++;
            gameInfo.Game = null;
            _unitOfWork.GameInfoRepository.Update(gameInfo);
            _unitOfWork.Save();
        }

        public PaginationGames Get(params Expression<Func<Game, object>>[] includeProperties)
        {
            var games = new PaginationGames()
            {
                Count = _unitOfWork.GameRepository.GetCountObject(x => true),
                Games = _unitOfWork.GameRepository.Get(x => true, x => x.Id)
            };
            return games;
        }

        IEnumerable<Game> ICrudService<Game>.Get(params Expression<Func<Game, object>>[] includeProperties)
        {
            var games = _unitOfWork.GameRepository.Get(x => true, x => x.Id);
            return games;
        }
    }
}
