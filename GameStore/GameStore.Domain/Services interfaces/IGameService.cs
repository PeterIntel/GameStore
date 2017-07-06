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
        IList<Game> GetAll(Expression<Func<Game, bool>> filter, string includeProperties = "");
        Game GetItemByKey(string key);
    }
}
