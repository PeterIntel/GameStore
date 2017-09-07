using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GameStore.DataAccess.Interfaces;
using GameStore.DataAccess.MSSQL.Entities;
using GameStore.Domain.BusinessObjects;

namespace GameStore.DataAccess.MSSQL.Repositories
{
    public class GenreRepository : GenericDataRepository<GenreEntity, Genre>, IGenreRepository
    {
        public GenreRepository(GamesSqlContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public IEnumerable<GenreEntity> GetGenres(IEnumerable<Genre> genres)
        {
            var gen = from i in genres
                from genre in _dbSet
                where i.Id == genre.Id
                select genre;
 
            return gen;
        }
    }
}
