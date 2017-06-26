using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.contracts.DomainModels
{
    public class PlatformType : BasicEntity
    {
        public int Id { set; get; }
        public string Type { set; get; }
        public virtual IList<Game> Games { set; get; }
    }
}
