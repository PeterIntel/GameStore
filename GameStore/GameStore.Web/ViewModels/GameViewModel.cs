using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace GameStore.Web.ViewModels
{
    public class GameViewModel
    {
        [Required]
        public string Key { set; get; }
        [Required]
        public string Description { set; get; }
        public decimal Price { set; get; }
        public short UnitsInStock { set; get; }
        public bool Discontinued { set; get; }
        public IList<CommentViewModel> Comments { set; get; }
        public IList<GenreViewModel> Genres { set; get; }
        public IList<PlatformTypeViewModel> PlatformTypes { set; get; }
    }
}
