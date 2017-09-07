using System;
using System.Collections.Generic;
using System.Data.Entity;
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
                var genreEntity = _mapper.Map<Genre, GenreEntity>(genre);
                genreEntity.IsSqlEntity = true;
                var id = GetGuidId();
                genreEntity.Locals = new List<GenreLocalEntity>() {
                    new GenreLocalEntity()
                    {
                        Id = id,
                        Culture = _cultureRepository.GetCultureByCode(genre.Locals.First().Culture.Code),
                        Name = genre.Locals.First().Name
                    }
                };

                _dbSet.Add(genreEntity);
            }
        }

        public override void Update(Genre genre)
        {
            var currentGenre = _mapper.Map<Genre, GenreEntity>(genre);
            var existingGenre = _dbSet.Find(genre.Id);
            _mapper.Map(currentGenre, existingGenre);

            var currentCulture = genre.Locals.First().Culture.Code;
            var name = genre.Locals.First().Name;

            var local = existingGenre.Locals.FirstOrDefault(x => x.Culture.Code == currentCulture);

            if (local == null)
            {
                var id = GetGuidId();
                existingGenre.Locals.Add(

                    new GenreLocalEntity()
                    {
                        Id = id,
                        Culture = _cultureRepository.GetCultureByCode(currentCulture),
                        Name = name
                    }
                );
            }
            else
            {
                local.Name = name;
            }

            if (_context.Entry(existingGenre).State == EntityState.Detached)
            {
                _context.Genres.Attach(existingGenre);
            }

            _context.Entry(existingGenre).State = EntityState.Modified;
        }
    }
}
