using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Domain.BusinessObjects.LocalizationObjects;

namespace GameStore.Domain.ServicesInterfaces
{
    public interface ICultureService : ICrudService<Culture>
    {
        Culture GetByCultureCode(string cultureCode);
    }
}
