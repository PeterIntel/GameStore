using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GameStore.DataAccess.MSSQL.Entities.Localization;

namespace GameStore.DataAccess.MSSQL.Entities
{
    public class GenreEntity : BasicEntity 
    {
        public string ParentGenreId { set; get; }
        public virtual GenreEntity ParentGenre { set; get; }
        public virtual IList<GenreEntity> Genres { set; get; }
        public virtual IList<GameEntity> Games { set; get; }
        public virtual IList<GenreLocalEntity> Locals { get; set; }
    }
}
