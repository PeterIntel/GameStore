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
        public decimal Price { set; get; }
        public short UnitsInStock { set; get; }
        public bool Discontinued { set; get; }
        public DateTime? PublishedDate { set; get; }
        public virtual PublisherEntity Publisher { set; get; }
        public virtual GameInfoEntity GameInfo { set; get; }
        public virtual IList<CommentEntity> Comments { set; get; }

        public virtual IList<GenreEntity> Genres { set; get; }

        public virtual IList<PlatformTypeEntity> PlatformTypes { set; get; }
    }
}
