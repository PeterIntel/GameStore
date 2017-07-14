using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStore.Web.ViewModels
{
    public class CommentsViewModel
    {
        public IList<CommentViewModel> Comments { set; get; }
        public CommentViewModel Comment { set; get; }
    }
}