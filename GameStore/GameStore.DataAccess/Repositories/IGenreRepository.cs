using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.DataAccess.Entities;
using GameStore.Domain.BusinessObjects;

namespace GameStore.DataAccess.Repositories
{
    public interface IGenreRepository : IGenericDataRepository<GenreEntity, Genre>
    {
        IEnumerable<GenreEntity> GetGenres(IList<string> genres);
    }
}
