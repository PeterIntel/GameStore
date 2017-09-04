using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DataAccess.MSSQL.Entities.Localization
{
    public class GameLocalEntity : AbstractLocalizationEntity
    {
        public string GameId { get; set; }

        public virtual GameEntity Game { get; set; }

        public string Description { get; set; }
    }
}
