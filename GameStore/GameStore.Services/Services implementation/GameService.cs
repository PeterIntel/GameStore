using GameStore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.DataAccess.UnitOfWork;
using GameStore.Domain.Business_objects;
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
            _unitOfWork.Games.Add(item);
            _unitOfWork.Save();
        }

        public IList<Game> GetAll(System.Linq.Expressions.Expression<Func<Game, bool>> filter, string includeProperties = "")
        {
            return _unitOfWork.Games.GetAll(filter, includeProperties);
        }

        public Game GetItemByKey(string key)
        {
            return _unitOfWork.Games.GetAll(x => x.Key == key).First();
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
