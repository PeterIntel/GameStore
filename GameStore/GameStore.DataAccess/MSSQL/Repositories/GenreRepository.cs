using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GameStore.DataAccess.Interfaces;
using GameStore.DataAccess.MSSQL.Entities;
using GameStore.DataAccess.MSSQL.Entities.Localization;
using GameStore.Domain.BusinessObjects;

namespace GameStore.DataAccess.MSSQL.Repositories
{
    public class GenreRepository : GenericDataRepository<GenreEntity, Genre>, IGenreRepository
    {
        private readonly ICultureRepository _cultureRepository;
        public GenreRepository(GamesSqlContext context, IMapper mapper, ICultureRepository cultureRepository) : base(context, mapper)
        {
            _cultureRepository = cultureRepository;
        }

        public IEnumerable<GenreEntity> GetGenres(IEnumerable<Genre> genres)
        {
            var gen = from i in genres
                from genre in _dbSet
                where i.Id == genre.Id
                select genre;
 
            return gen;
        }

        public override void Add(Genre genre)
        {
            if (genre != null)
            {
                var d = _cultureRepository.GetCultureByCode(genre.Locals.First().Culture.Code);
                var genreEntity = _mapper.Map<Genre, GenreEntity>(genre);
                genreEntity.IsSqlEntity = true;
                var id = Guid.NewGuid().ToString();
                genreEntity.Locals.Add(new GenreLocalEntity()
                {
                    Id = id,
                    Culture = d,
                    Name = genre.Locals.First().Name
                });

                _dbSet.Add(genreEntity);
            }
        }
    }
}
