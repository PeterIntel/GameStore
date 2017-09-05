using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.DataAccess.Interfaces;
using GameStore.DataAccess.MSSQL.Entities;
using GameStore.Domain.BusinessObjects;

namespace GameStore.Services.Localization.Specific
{
    public class GenreLocalizationProvider : ILocalizationProvider<Genre>
    {
        public Genre Localize(Genre genre, string cultureCode)
        {
            if (genre.Locals != null && genre.Locals.Any())
            {
                var local = genre.Locals.FirstOrDefault(x => x.Culture.Code == cultureCode) ?? genre.Locals.First();

                genre.Name = local.Name;
                if (genre.ParentGenre != null)
                {
                    Localize(genre.ParentGenre, cultureCode);
                }
            }
            return genre;
        }
    }
}
