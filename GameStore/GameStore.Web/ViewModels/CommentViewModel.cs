using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Web.ViewModels
{
    public class CommentViewModel
    {
        public string Id { set; get; }
        [Required]
        public string Name { set; get; }
        [Required]
        public string Body { set; get; }
        public string ParentCommentId { set; get; }
        public bool IsDisabled { set; get; }
        public bool IsDeletedGame { set; get; }
        public string GameId { set; get; }
        public string GameKey { set; get; }
        public GameViewModel Game { set; get; }
        public CommentViewModel ParentComment { set; get; }
        public IList<CommentViewModel> Comments { set; get; }
    }
}
