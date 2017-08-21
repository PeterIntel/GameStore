using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Domain.BusinessObjects;

namespace GameStore.Authorization
{
    public interface IUserProvider
    {
        User User { set; get; }
    }
}
