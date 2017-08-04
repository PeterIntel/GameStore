using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GameStore.DataAccess.Entities
{
    public class GenreEntity : BasicEntity 
    {
        [Index(IsUnique = true)]
        [StringLength(450)]
        public string Name { set; get; }
        public string ParentGenreId { set; get; }
        public virtual IList<GenreEntity> Genres { set; get; }
        public virtual IList<GameEntity> Games { set; get; }
    }
}
