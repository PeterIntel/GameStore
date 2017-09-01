using System;
using System.Collections.Generic;

namespace GameStore.Domain.BusinessObjects
{
    public class FilterOrders
    {
        public TimeSpan? PeriodDate { set; get; }
        public DateTimeIntervalFlag DateTimeIntervalFlag { set; get; }
        public DateTime? DateFrom { set; get; }
        public DateTime? DateTo { set; get; }
        public IEnumerable<Order> Orders { set; get; }
    }
}
