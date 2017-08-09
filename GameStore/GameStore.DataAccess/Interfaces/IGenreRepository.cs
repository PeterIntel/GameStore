using System.Collections.Generic;
using GameStore.DataAccess.MSSQL.Entities;
using GameStore.Domain.BusinessObjects;

namespace GameStore.DataAccess.Interfaces
{
    public interface IGenreRepository : IGenericDataRepository<GenreEntity, Genre>
    {
        IEnumerable<GenreEntity> GetGenres(IList<string> genres);
    }
}
