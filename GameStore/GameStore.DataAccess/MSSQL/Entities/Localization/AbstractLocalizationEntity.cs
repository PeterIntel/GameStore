using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DataAccess.MSSQL.Entities.Localization
{
    public abstract class AbstractLocalizationEntity
    {
        public string Id { get; set; }

        public string CultureId { get; set; }

        public virtual CultureEntity Culture { get; set; }
    }
}
