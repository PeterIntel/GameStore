using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.contracts.DomainModels
{
    public class Game : BasicEntity
    {
        public int Id { set; get; }
        public string Key { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public virtual IList<Comment> Comments { set; get; }
        public virtual IList<Genre> Genres { set; get; }
        public virtual IList<PlatformType> PlatformTypes { set; get; }
    }
}
