using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Web.ViewModels
{
    public class GameViewModel
    {
        public int Id { set; get; }
        public string Key { set; get; }
        public string Description { set; get; }
        public IList<CommentViewModel> Comments { set; get; }
        public virtual IList<GenreViewModel> Genres { set; get; }
        public virtual IList<PlatformTypeViewModel> PlatformTypes { set; get; }
    }
}
