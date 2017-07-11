using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Domain.ServicesInterfaces;
using GameStore.Domain.BusinessObjects;
using System.Linq.Expressions;

namespace GameStore.Domain.ServicesInterfaces
{
    public interface IGenreService: ICrudService<Genre>
    {
        IEnumerable<Genre> GetAll(params Expression<Func<Genre, object>>[] includeProperties);
        IEnumerable<Genre> GetGenres(IList<string> genres);
    }
}
