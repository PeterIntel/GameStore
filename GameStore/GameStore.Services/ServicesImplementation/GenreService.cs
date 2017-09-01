using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GameStore.DataAccess.Interfaces;
using GameStore.DataAccess.MSSQL.Entities;
using GameStore.DataAccess.UnitOfWork;
using GameStore.Domain.BusinessObjects;
using GameStore.Domain.ServicesInterfaces;
using GameStore.Logging.Loggers;

namespace GameStore.Services.ServicesImplementation
{
    public class GenreService : BasicService<GenreEntity, Genre>, IGenreService
    {
        private readonly IGenericDataRepository<GenreEntity, Genre> _genreRepository;
        
        public GenreService(IUnitOfWork unitOfWork, IGenericDataRepository<GenreEntity, Genre> genreRepository, IMongoLogger<Genre> logger) : base(genreRepository, unitOfWork, logger)
        {
            _genreRepository = genreRepository;
        }

        public override IEnumerable<Genre> Get(params Expression<Func<Genre, object>>[] includeProperties)
         {
            var genres = base.Get(includeProperties).ToList();
            foreach (var genre in genres)
            {
                if (genre.ParentGenreId != null)
                {
                    genre.ParentGenreName = _genreRepository.First(g => g.Id == genre.ParentGenreId).Name;
                }
            }

            return genres;
        }

        public override Genre First(Expression<Func<Genre, bool>> filter)
        {
            var genre =  base.First(filter);
            genre.ParentGenreName = _genreRepository.First(g => g.Id == genre.Id).Name;

            return genre;
        }

        public IEnumerable<Genre> GetAllGenresAndMarkSelected(IEnumerable<string> selecredGenres)
        {
            var genres = GetAllGenresAndMarkSelectedForFilter(selecredGenres).Where(genre => genre.Name != "Other");

            return genres;
        }

        public IEnumerable<Genre> GetAllGenresAndMarkSelectedForFilter(IEnumerable<string> selecredGenres)
        {
            IEnumerable<Genre> genres = _genreRepository.Get().ToList();
            if (selecredGenres != null)
            {
                foreach (var item in genres)
                {
                    if (selecredGenres.Contains(item.Name))
                    {
                        item.IsChecked = true;
                    }
                }
            }

            return genres;
        }
    }
}
