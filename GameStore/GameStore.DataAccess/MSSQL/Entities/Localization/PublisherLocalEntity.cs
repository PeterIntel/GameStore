using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DataAccess.MSSQL.Entities.Localization
{
    public class PublisherLocalEntity : AbstractLocalizationEntity
    {
        public string PublisherId { get; set; }

        public virtual PublisherEntity Publisher { get; set; }

        public string Description { get; set; }
    }
}
