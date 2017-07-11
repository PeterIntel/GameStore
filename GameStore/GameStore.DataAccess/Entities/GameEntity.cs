using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GameStore.DataAccess.Entities
{
    public class GameEntity : BasicEntity
    {
        public int Id { set; get; }

        [Index(IsUnique = true)]
        [StringLength(450)]
        public string Key { set; get; }
        public string Description { set; get; }

        public virtual IEnumerable<CommentEntity> Comments { set; get; }

        public virtual IEnumerable<GenreEntity> Genres { set; get; }

        public virtual IEnumerable<PlatformTypeEntity> PlatformTypes { set; get; }
    }
}
