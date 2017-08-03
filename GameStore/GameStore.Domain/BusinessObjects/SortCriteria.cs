using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.BusinessObjects
{
    public enum SortCriteria
    {
        Default,
        MostPopular,
        MostCommented,
        ByPriceAsc,
        ByPriceDesc,
        New
    }
}
