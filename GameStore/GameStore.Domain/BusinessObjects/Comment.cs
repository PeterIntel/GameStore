using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.BusinessObjects
{
    public class Comment : BasicDomain
    {
        public string Name { set; get; }
        public string Body { set; get; }
        public string ParentCommentId { set; get; }
        public string GameId { set; get; }
        public Game Game { set; get; }
        public Comment ParentComment { set; get; }
        public IEnumerable<Comment> Comments { set; get; }
    }
}
