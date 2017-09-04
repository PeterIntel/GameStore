using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DataAccess.MSSQL.Entities.Localization
{
    public class GenreLocalEntity : AbstractLocalizationEntity
    {
        public string GenreId { get; set; }

        public virtual GenreEntity Genre { get; set; }

        public string Name { get; set; }
    }
}
