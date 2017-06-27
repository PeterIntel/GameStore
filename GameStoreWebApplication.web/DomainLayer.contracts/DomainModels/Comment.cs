using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.contracts.DomainModels
{
    public class Comment : BasicEntity
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Body { set; get; }
        public int? ParentCommentId { set; get; }
        public int? GameId { set; get; }
        public virtual Game Game { set; get; }
        public virtual IList<Comment> Comments { set; get; }
    }
}
