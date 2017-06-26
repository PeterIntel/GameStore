using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.contracts.DomainModels;
using System.Linq.Expressions;

namespace GameStore.services.Services
{
    public interface IGameService : IService<Game>
    {
        IList<Game> GetAll(Expression<Func<Game, bool>> filter, string includeProperties = "");
        Game GetItemById(int id);
    }
}
