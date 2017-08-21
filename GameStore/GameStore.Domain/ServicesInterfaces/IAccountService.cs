using GameStore.Domain.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.ServicesInterfaces
{
    public interface IAccountService : ICrudService<User>
    {
    }
}
