using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GameStore.Domain.BusinessObjects;

namespace GameStore.Services.ServicesImplementation.FilterImplementation.OrderFilter
{
    public class OrderPipeLine : Pipeline<Order>
    {
        public override Expression<Func<Order, bool>> Process(Expression<Func<Order, bool>> input)
        {
            foreach (var filter in filters)
            {
                input = filter.Execute(input);
            }

            return input;
        }

        public Expression<Func<Order, bool>> ApplyFilters(FilterOrders filters)
        {

            Register(new DateFromFilter(filters.DateFrom));

            Register(new DateToFilter(filters.DateTo));

            Register(new PeriodDateFilter(filters.PeriodDate, filters.DateTimeIntervalFlag));

            return Process(x => true);
        }
    }
}
