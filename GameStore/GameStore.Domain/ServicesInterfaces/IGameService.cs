using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using GameStore.Domain.BusinessObjects;

namespace GameStore.Domain.ServicesInterfaces
{
    public interface IGameService : ICrudService<Game>
    {
        Game GetItemByKey(string key);
        void AddViewToGame(string key);
        IEnumerable<Game> FilterGames(FilterCriteria filter, int? page, int? size, out int count);
    }
}
