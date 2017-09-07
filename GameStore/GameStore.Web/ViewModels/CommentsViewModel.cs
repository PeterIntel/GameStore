using System.Collections.Generic;

namespace GameStore.Web.ViewModels
{
    public class CommentsViewModel
    {
        public IList<CommentViewModel> Comments { set; get; }

        public CommentViewModel Comment { set; get; }
    }
}