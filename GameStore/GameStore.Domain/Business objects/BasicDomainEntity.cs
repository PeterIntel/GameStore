using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Business_objects
{
    public abstract class BasicDomainEntity
    {
        public bool IsDeleted { set; get; }
    }
}
