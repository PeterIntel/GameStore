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
        public string Id { set; get; }
        public string Name { set; get; }
        public string ParentGenreId { set; get; }
        public bool IsChecked { set; get; }
        public IList<GenreViewModel> Genres { set; get; }
        public IList<GameViewModel> Games { set; get; }
    }
}
