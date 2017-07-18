using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Web.ViewModels
{
    public class CommentViewModel
    {
        public int Id { set; get; }
        [Required]
        public string Name { set; get; }
        [Required]
        public string Body { set; get; }
        public int? ParentCommentId { set; get; }
        public string GameKey { set; get; }
        public GameViewModel Game { set; get; }
        public IList<CommentViewModel> Comments { set; get; }
    }
}
