using GameStore.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDataAccessLayer.DAL.UnitOfWork;
using DomainLayer.contracts.DomainModels;

namespace GameStore.services.Services
{
    class GameService : IGameService
    {
        private IUnitOfWork _unitOfWork;
        public GameService()
        {
            _unitOfWork = new UnitOfWork();
        }
        public void Add(Game item)
        {
            _unitOfWork.Games.Add(item);
            _unitOfWork.Save();
        }

        public IList<Game> GetAll(System.Linq.Expressions.Expression<Func<Game, bool>> filter, string includeProperties = "")
        {
            return _unitOfWork.Games.GetAll(filter, includeProperties);
        }

        public Game GetItemById(int id)
        {
            return _unitOfWork.Games.GetItemById(id);
        }

        public void Remove(int id)
        {
            _unitOfWork.Games.Remove(id);
            _unitOfWork.Save();
        }

        public void Remove(Game item)
        {
            _unitOfWork.Games.Remove(item);
            _unitOfWork.Save();
        }

        public void Update(Game item)
        {
            _unitOfWork.Games.Update(item);
            _unitOfWork.Save();
        }
    }
}
