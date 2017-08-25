using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GameStore.DataAccess.Interfaces;
using GameStore.DataAccess.Mongo.MongoEntities;
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

        public IEnumerable<Genre> GetAllGenresAndMarkSelected(IEnumerable<string> selecredGenres)
        {
            IEnumerable<Genre> genres = _genreRepository.Get().ToList();
            if (selecredGenres != null)
            {
                foreach (var item in genres)
                {
                    if (selecredGenres.Contains(item.Id))
                    {
                        item.IsChecked = true;
                    }
                }
            }
            return genres;
        }
    }
}
