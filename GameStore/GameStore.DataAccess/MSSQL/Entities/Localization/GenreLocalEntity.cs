using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DataAccess.MSSQL.Entities.Localization
{
    public class GenreLocalEntity : AbstractLocalizationEntity
    {
        public string GenreId { get; set; }

        public virtual GenreEntity Genre { get; set; }
        [Index(IsUnique = true)]
        [StringLength(450)]
        public string Name { get; set; }
    }
}
