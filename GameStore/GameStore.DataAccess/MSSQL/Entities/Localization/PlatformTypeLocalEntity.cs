using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.DataAccess.MSSQL.Entities.Localization
{
    public class PlatformTypeLocalEntity : AbstractLocalizationEntity
    {
        public string PlatformTypeId { get; set; }

        public virtual PlatformTypeEntity PlatformType { get; set; }
        [Index(IsUnique = true)]
        [StringLength(450)]
        public string TypeName { get; set; }
    }
}
