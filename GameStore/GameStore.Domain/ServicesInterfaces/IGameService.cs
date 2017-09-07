﻿using System;
using System.Linq.Expressions;
using GameStore.Domain.BusinessObjects;

namespace GameStore.Domain.ServicesInterfaces
{
    public interface IGameService : ICrudService<Game>
    {
        Game GetItemByKey(string key);

        void AddViewToGame(string key);

        PaginationGames FilterGames(FilterCriteria filters, int page, string size);

        new PaginationGames Get(params Expression<Func<Game, object>>[] includeProperties);
    }
}