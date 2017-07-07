using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Web.ViewModels
{
    public class GenreViewModel
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public int? ParentGenreId { set; get; }
        public IList<GenreViewModel> Genres { set; get; }
        public virtual IList<GameViewModel> Games { set; get; }
    }
}
