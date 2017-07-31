using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.DataAccess.Contextes;
using GameStore.DataAccess.Entities;
using GameStore.Domain.BusinessObjects;

namespace GameStore.DataAccess.Repositories
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
