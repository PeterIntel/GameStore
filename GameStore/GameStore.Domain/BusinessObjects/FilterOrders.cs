using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.BusinessObjects
{
    public class FilterOrders
    {
        public DateTime? DateFrom { set; get; }
        public DateTime? DateTo { set; get; }
        public IEnumerable<Order> Orders { set; get; }
    }
}
