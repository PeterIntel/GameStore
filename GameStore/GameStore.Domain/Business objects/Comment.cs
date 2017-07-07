using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.BusinessObjects
{
    public class Comment : BasicDomainEntity
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Body { set; get; }
        public int? ParentCommentId { set; get; }
        public int? GameId { set; get; }
        public Game Game { set; get; }
        public IList<Comment> Comments { set; get; }
    }
}
