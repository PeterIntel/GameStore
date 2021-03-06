﻿using System.Collections.Generic;
using GameStore.Domain.BusinessObjects;

namespace GameStore.Domain.ServicesInterfaces
{
    public interface IGenreService: ICrudService<Genre>
    {
        IEnumerable<Genre> GetAllGenresAndMarkSelected(IEnumerable<string> selecredGenres, string cultureCode);
        IEnumerable<Genre> GetAllGenresAndMarkSelectedForFilter(IEnumerable<string> selecredGenres, string cultureCode);
        Genre GetFirstGenreByName(string key, string cultureCode);
    }
}
