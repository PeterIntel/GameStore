using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using GameStore.Domain.BusinessObjects;

namespace GameStore.Domain.Services_interfaces
{
    public interface IGameService : ICrudService<Game>
    {
        IEnumerable<Game> GetAll(Expression<Func<Game, bool>> filter, params Expression<Func<Game, object>>[] includeProperties);
        IEnumerable<Game> GetAll(params Expression<Func<Game, object>>[] includeProperties);
        Game GetItemByKey(string key);
    }
}
