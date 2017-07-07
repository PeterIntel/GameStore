using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Web.ViewModels
{
    public class CommentViewModel
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Body { set; get; }
        public int? ParentCommentId { set; get; }
        public int? GameId { set; get; }
        public GameViewModel Game { set; get; }
        public IList<CommentViewModel> Comments { set; get; }
    }
}
