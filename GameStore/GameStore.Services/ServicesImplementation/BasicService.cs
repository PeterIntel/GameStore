using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GameStore.Domain.BusinessObjects;
using GameStore.Domain.ServicesInterfaces;

namespace GameStore.Services.ServicesImplementation
{
    public abstract class BasicService<TDomain> where TDomain : BasicDomain
    {
        public void AssignIdIfEmpty(TDomain item)
        {
            if (item.Id == null)
            {
                item.Id = Guid.NewGuid().ToString();
            }
        }
    }
}
