using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GameStore.DataAccess.MSSQL.Entities;
using GameStore.Domain.BusinessObjects;

namespace GameStore.DataAccess.MSSQL.Repositories
{
    public class GenreRepository : GenericDataRepository<GenreEntity, Genre>, IGenreRepository
    {
        public GenreRepository(GamesSqlContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public IEnumerable<GenreEntity> GetGenres(IList<string> genres)
        {
            var gen = from i in genres
                from genre in _dbSet
                where i == genre.Name
                select genre;
            return gen;
        }
    }
}
