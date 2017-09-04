using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DataAccess.MSSQL.Entities.Localization
{
    public class PlatformTypeLocalEntity : AbstractLocalizationEntity
    {
        public string PlatformTypeId { get; set; }

        public virtual PlatformTypeEntity PlatformType { get; set; }

        public string Name { get; set; }
    }
}
