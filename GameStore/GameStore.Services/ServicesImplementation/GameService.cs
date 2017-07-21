using System;
using System.Collections.Generic;
using System.Linq;
using GameStore.DataAccess.UnitOfWork;
using GameStore.Domain.BusinessObjects;
using GameStore.Domain.ServicesInterfaces;
using System.Linq.Expressions;

namespace GameStore.Services.ServicesImplementation
{
    public class GameService : IGameService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICommentService _commentService;
        public GameService(IUnitOfWork unitOfWork, ICommentService commentService)
        {
            _unitOfWork = unitOfWork;
            _commentService = commentService;
        }
        public void Add(Game item)
        {
            _unitOfWork.GameRepository.Add(item);
            _unitOfWork.Save();
        }

        public IEnumerable<Game> GetAll(Expression<Func<Game, bool>> filter, params Expression<Func<Game, object>>[] includeProperties)
        {
            return _unitOfWork.GameRepository.GetAll(filter, includeProperties);
        }

        public IEnumerable<Game> GetAll(params Expression<Func<Game, object>>[] includeProperties)
        {
            var games = _unitOfWork.GameRepository.GetAll(includeProperties);
            return games;
        }

        public Game GetItemByKey(string key)
        {
            var result = _unitOfWork.GameRepository.GetGameByKey(key);
            return result;
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
    }
}
