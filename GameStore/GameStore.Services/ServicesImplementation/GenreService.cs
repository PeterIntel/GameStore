using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GameStore.DataAccess.Decorators;
using GameStore.DataAccess.Mongo.MongoEntities;
using GameStore.DataAccess.MSSQL.Entities;
using GameStore.DataAccess.UnitOfWork;
using GameStore.Domain.BusinessObjects;
using GameStore.Domain.ServicesInterfaces;

namespace GameStore.Services.ServicesImplementation
{
    public class GenreService : IGenreService
    {
        private readonly IGenericDecoratorRepository<GenreEntity, MongoCategoryEntity, Genre> _genreRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GenreService(IUnitOfWork unitOfWork, IGenericDecoratorRepository<GenreEntity, MongoCategoryEntity, Genre> genreRepository)
        {
            _unitOfWork = unitOfWork;
            _genreRepository = genreRepository;
        }
        public void Add(Genre item)
        {
            _genreRepository.Add(item);
            _unitOfWork.Save();
        }

        public IEnumerable<Genre> Get(params Expression<Func<Genre, object>>[] includeProperties)
        {
            return _genreRepository.Get(includeProperties).ToList();
        }

        public void Remove(string id)
        {
            _genreRepository.Remove(id);
            _unitOfWork.Save();
        }

        public void Remove(Genre item)
        {
            _genreRepository.Remove(item);
            _unitOfWork.Save();
        }

        public void Update(Genre item)
        {
            _genreRepository.Update(item);
            _unitOfWork.Save();
        }

        public IEnumerable<Genre> GetAllGenresAndMarkSelected(IEnumerable<string> selecredGenres)
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
