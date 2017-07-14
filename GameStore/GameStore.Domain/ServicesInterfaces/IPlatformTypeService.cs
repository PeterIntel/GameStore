using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Domain.BusinessObjects;
using System.Linq.Expressions;

namespace GameStore.Domain.ServicesInterfaces
{
    public interface IPlatformTypeService : ICrudService<PlatformType>
    {
        IEnumerable<PlatformType> GetAll(params Expression<Func<PlatformType, object>>[] includeProperties);
    }
}
