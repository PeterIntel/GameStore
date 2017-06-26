using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.contracts.DomainModels
{
    public abstract class BasicEntity
    {
        public bool IsDeleted { set; get; }
    }
}
