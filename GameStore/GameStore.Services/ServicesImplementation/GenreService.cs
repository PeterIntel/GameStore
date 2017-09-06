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
using GameStore.Services.Localization;

namespace GameStore.Services.ServicesImplementation
{
    public class GenreService : BasicService<GenreEntity, Genre>, IGenreService
    {
        private readonly IGenericDataRepository<GenreEntity, Genre> _genreRepository;

        public GenreService(IUnitOfWork unitOfWork, IGenericDataRepository<GenreEntity, Genre> genreRepository, IMongoLogger<Genre> logger,
            ILocalizationProvider<Genre> localizatorProvider) : base(genreRepository, unitOfWork, logger, localizatorProvider)
        {
            _genreRepository = genreRepository;
        }

        public override IEnumerable<Genre> Get(string cultureCode, params Expression<Func<Genre, object>>[] includeProperties)
         {
            var genres = base.Get(cultureCode, includeProperties).ToList();
            foreach (var genre in genres)
            {
                LocalizationProvider.Localize(genre, cultureCode);
                if (genre.ParentGenreId != null)
                {
                    genre.ParentGenreName = _genreRepository.First(g => g.Id == genre.ParentGenreId).Name;
                }
            }

            return genres;
        }

        public  Genre GetFirstGenreByName(string key, string cultureCode)
        {
            var genre = _genreRepository.First(x => x.Locals.Any(y => y.Name == key));
            LocalizationProvider.Localize(genre, cultureCode);
            genre.ParentGenreName = LocalizationProvider.Localize(_genreRepository.First(g => g.Id == genre.Id), cultureCode).Name;

            return genre;
        }

        public IEnumerable<Genre> GetAllGenresAndMarkSelected(IEnumerable<string> selecredGenres, string cultureCode)
        {
            var genres = GetAllGenresAndMarkSelectedForFilter(selecredGenres, cultureCode).Where(genre => genre.Locals.Any(g => g.Name != "Other"));

            foreach (var genre in genres)
            {
                LocalizationProvider.Localize(genre, cultureCode);
            }

            return genres;
        }

        public IEnumerable<Genre> GetAllGenresAndMarkSelectedForFilter(IEnumerable<string> selecredGenres, string cultureCode)
        {
            IEnumerable<Genre> genres = _genreRepository.Get().ToList();

            foreach (var genre in genres)
            {
                LocalizationProvider.Localize(genre, cultureCode);
            }

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
