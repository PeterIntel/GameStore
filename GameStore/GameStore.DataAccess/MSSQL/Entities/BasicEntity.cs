using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.DataAccess.MSSQL.Entities
{
    public abstract class BasicEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { set; get; }
        public bool IsDeleted { set; get; }
        public bool IsMongoEntity { set; get; }
    }
}
