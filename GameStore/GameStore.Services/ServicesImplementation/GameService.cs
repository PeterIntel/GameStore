using GameStore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.DataAccess.UnitOfWork;
using GameStore.Domain.BusinessObjects;
using GameStore.Domain.Services_interfaces;
using System.Linq.Expressions;

namespace GameStore.Services.ServicesImplementation
{
    public class GameService : IGameService
    {
        private IUnitOfWork _unitOfWork;
        public GameService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
            return _unitOfWork.GameRepository.GetAll(includeProperties);
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
