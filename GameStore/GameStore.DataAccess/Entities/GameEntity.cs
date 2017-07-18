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
        [Index(IsUnique = true)]
        [StringLength(450)]
        [Key]
        public string Key { set; get; }
        public string Description { set; get; }
        public decimal Price { set; get; }
        public short UnitsInStock { set; get; }
        public bool Discontinued { set; get; }
        public virtual PublisherEntity Publisher { set; get; }
        public virtual IList<CommentEntity> Comments { set; get; }

        public virtual IList<GenreEntity> Genres { set; get; }

        public virtual IList<PlatformTypeEntity> PlatformTypes { set; get; }
    }
}
