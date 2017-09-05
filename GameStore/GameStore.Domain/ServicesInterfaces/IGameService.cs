using System;
using System.Linq.Expressions;
using GameStore.Domain.BusinessObjects;

namespace GameStore.Domain.ServicesInterfaces
{
    public interface IGameService : ICrudService<Game>
    {
        Game GetItemByKey(string key, string cultureCode);
        void AddViewToGame(string key, string cultureCode);
        PaginationGames FilterGames(FilterCriteria filters, int page, string size, string cultureCode);
        PaginationGames Get(string cultureCode, params Expression<Func<Game, object>>[] includeProperties);
    }
}
