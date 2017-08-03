using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DataAccess.Entities
{
    public abstract class BasicEntity
    {
        public int Id { set; get; }
        public bool IsDeleted { set; get; }
    }
}
