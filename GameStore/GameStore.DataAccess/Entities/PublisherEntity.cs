using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.DataAccess.Entities
{
    public class PublisherEntity : BasicEntity
    {
        [Index(IsUnique = true)]
        public string CompanyName { set; get; }
        public string Description { set; get; }
        public string HomePage { set; get; }
        public virtual IList<GameEntity> Games { set; get; }
    }
}
