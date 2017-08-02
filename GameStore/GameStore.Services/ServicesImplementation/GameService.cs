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

        public IEnumerable<Game> FilterGames(FilterCriteria filters, out int count, int page, int size)
        {
            _gamePipeline = new GamePipeline();
            _gamePipeline.ApplyFilters(filters);
            var filterExpression = _gamePipeline.Process(x => true);
            count = _unitOfWork.GameRepository.GetCountObject(_gamePipeline.Process(x => true));

            switch (filters.SortCriteria)
            {
                case SortCriteria.ByPriceAsc:
                    return _unitOfWork.GameRepository.Get(_gamePipeline.Process(x => true), x => x.Price, page, size);
                case SortCriteria.ByPriceDesc:
                    return _unitOfWork.GameRepository.Get(_gamePipeline.Process(x => true), x => x.Price * (-1), page, size);
                case SortCriteria.MostCommented:
                    return _unitOfWork.GameRepository.Get(_gamePipeline.Process(x => true), x => x.Comments.Count() * (-1), page, size);
                case SortCriteria.New:
                    return _unitOfWork.GameRepository.Get(_gamePipeline.Process(x => true), x => x.GameInfo.UploadDate, page, size);
                case SortCriteria.MostPopular:
                    return _unitOfWork.GameRepository.Get(_gamePipeline.Process(x => true), x => x.GameInfo.CountOfViews * (-1), page, size);
            }
            return _unitOfWork.GameRepository.Get(_gamePipeline.Process(x => true), x => x.Id, page, size);
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

        public IEnumerable<Game> Get(params Expression<Func<Game, object>>[] includeProperties)
        {
            var games = _unitOfWork.GameRepository.Get();
            return games;
        }

        public IEnumerable<Game> Get(out int count, params Expression<Func<Game, object>>[] includeProperties)
        {
            count = _unitOfWork.GameRepository.GetCountObject(x => true);
            var games = _unitOfWork.GameRepository.Get(x => true,  x => x.Id);
            return games;
        }
    }
}
