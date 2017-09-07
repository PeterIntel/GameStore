using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GameStore.DataAccess.MSSQL.Entities.Localization;

namespace GameStore.DataAccess.MSSQL.Entities
{
    public class PlatformTypeEntity : BasicEntity
    {
        public virtual IList<GameEntity> Games { set; get; }
        public virtual IList<PlatformTypeLocalEntity> Locals { get; set; }
    }
}
