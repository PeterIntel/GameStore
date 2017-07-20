using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DataAccess.Entities
{
    public class CommentEntity : BasicEntity
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Body { set; get; }
        public int? ParentCommentId { set; get; }
        public int GameId { set; get; }
        public virtual GameEntity Game { set; get; }
        public virtual IList<CommentEntity> Comments { set; get; }
    }
}
