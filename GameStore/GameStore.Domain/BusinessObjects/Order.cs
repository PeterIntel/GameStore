using System;
using System.Collections.Generic;

namespace GameStore.Domain.BusinessObjects
{
    public class Order : BasicDomain
    {
        public string CustomerId { set; get; }
        public DateTime OrderDate { set; get; }
        public CompletionStatus Status { set; get; } = CompletionStatus.Complete;
        public IList<OrderDetails> OrderDetails { set; get; }
    }
}
