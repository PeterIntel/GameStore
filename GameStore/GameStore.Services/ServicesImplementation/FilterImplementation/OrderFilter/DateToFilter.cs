using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GameStore.Domain.BusinessObjects;

namespace GameStore.Services.ServicesImplementation.FilterImplementation.OrderFilter
{
    public class DateToFilter : BaseFilter<Order>
    {
        private readonly DateTime? _dateTo;

        public DateToFilter(DateTime? dateTo)
        {
            _dateTo = dateTo;
        }

        public override Expression<Func<Order, bool>> Execute(Expression<Func<Order, bool>> input)
        {
            if (_dateTo != null)
            {
                Expression<Func<Order, bool>> filter = x => x.OrderDate <= _dateTo;
                return AggregateExpression(input, filter);
            }
            return input;
        }
    }
}
