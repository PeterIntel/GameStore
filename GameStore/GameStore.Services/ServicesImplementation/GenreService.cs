using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GameStore.DataAccess.UnitOfWork;
using GameStore.Domain.BusinessObjects;
using GameStore.Domain.ServicesInterfaces;

namespace GameStore.Services.ServicesImplementation
{
    public class GenreService : IGenreService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenreService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Add(Genre item)
        {
            _unitOfWork.GenreRepository.Add(item);
            _unitOfWork.Save();
        }

        public IEnumerable<Genre> GetAll(params Expression<Func<Genre, object>>[] includeProperties)
        {
            return _unitOfWork.GenreRepository.GetAll(includeProperties);
        }

        public IEnumerable<Genre> GetGenres(IList<string> genres)
        {
            var gen = from i in genres
                from genre in _unitOfWork.GenreRepository.GetAll()
                where i == genre.Name
                select genre;
            return gen;
        }

        public void Remove(int id)
        {
            _unitOfWork.GenreRepository.Remove(id);
            _unitOfWork.Save();
        }

        public void Remove(Genre item)
        {
            _unitOfWork.GenreRepository.Remove(item);
            _unitOfWork.Save();
        }

        public void Update(Genre item)
        {
            _unitOfWork.GenreRepository.Update(item);
            _unitOfWork.Save();
        }
    }
}
