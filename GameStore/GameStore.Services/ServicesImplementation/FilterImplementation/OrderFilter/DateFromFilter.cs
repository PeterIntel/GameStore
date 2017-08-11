using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GameStore.Domain.BusinessObjects;

namespace GameStore.Services.ServicesImplementation.FilterImplementation.OrderFilter
{
    public class DateFromFilter : BaseFilter<Order>
    {
        private readonly DateTime? _dateFrom;

        public DateFromFilter(DateTime? dateFrom)
        {
            _dateFrom = dateFrom;
        }

        public override Expression<Func<Order, bool>> Execute(Expression<Func<Order, bool>> input)
        {
            if (_dateFrom != null)
            {
                Expression<Func<Order, bool>> filter = x => x.OrderDate >= _dateFrom;
                return AggregateExpression(input, filter);
            }
            return input;
        }
    }
}
