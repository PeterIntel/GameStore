using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using GameStore.DataAccess.MSSQL.Entities.Localization;

namespace GameStore.DataAccess.MSSQL.Entities
{
    public class PublisherEntity : BasicEntity
    {
        [Index(IsUnique = true)]
        public string CompanyName { set; get; }
        public string Description { set; get; }
        public string HomePage { set; get; }
        public virtual IList<GameEntity> Games { set; get; }
        public virtual IList<PublisherLocalEntity> Locals { get; set; }
    }
}
