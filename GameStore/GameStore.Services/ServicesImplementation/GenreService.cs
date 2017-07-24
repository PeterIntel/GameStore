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

        public IEnumerable<Genre> GetAllGenresAndMarkSelected(IEnumerable<string> selecredGenres)
        {
            IEnumerable<Genre> genres = _unitOfWork.GenreRepository.GetAll();
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
