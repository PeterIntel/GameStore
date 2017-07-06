using GameStore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.DataAccess.UnitOfWork;
using GameStore.Domain.BusinessObjects;
using GameStore.Domain.Services_interfaces;

namespace GameStore.Services.Services_implementation
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

        public IList<Game> GetAll(System.Linq.Expressions.Expression<Func<Game, bool>> filter, string includeProperties = "")
        {
            return _unitOfWork.GameRepository.GetAll(filter, includeProperties);
        }

        public Game GetItemByKey(string key)
        {
            var result = _unitOfWork.GameRepository.GetAll(x => x.Key == key);
            return result != null ? result.First() : null;
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
