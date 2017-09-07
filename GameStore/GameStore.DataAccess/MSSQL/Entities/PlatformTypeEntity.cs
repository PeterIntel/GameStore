using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.DataAccess.MSSQL.Entities
{
    public class PlatformTypeEntity : BasicEntity
    {
        [Index(IsUnique = true)]
        [StringLength(450)]
        public string TypeName { set; get; }

        public virtual IList<GameEntity> Games { set; get; }
    }
}
