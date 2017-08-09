using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.BusinessObjects
{
    public class Order : BasicDomain
    {
        public string CustomerId { set; get; }
        public DateTime OrderDate { set; get; }
        public CompletionStatus Status { set; get; }
        public IList<OrderDetails> OrderDetails { set; get; }
    }
}
