using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GameStore.DataAccess.Entities
{
    public class PlatformTypeEntity : BasicEntity
    {
        public int Id { set; get; }

        [Index(IsUnique = true)]
        [StringLength(450)]
        public string TypeName { set; get; }
        public virtual IEnumerable<GameEntity> Games { set; get; }
    }
}
