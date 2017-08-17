using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Domain.BusinessObjects;

namespace GameStore.Services.ServicesImplementation
{
    public abstract class BasicService<TDomain> where TDomain : BasicDomain
    {
        public TDomain AssignIdIfEmpty(TDomain item)
        {
            if (item.Id == null)
            {
                item.Id = Guid.NewGuid().ToString();
            }

            return item;
        }
    }
}
