using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.DataAccess.MSSQL.Entities
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
