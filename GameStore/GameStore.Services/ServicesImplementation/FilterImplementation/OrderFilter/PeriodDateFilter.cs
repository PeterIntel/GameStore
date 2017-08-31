using System;
using System.Linq.Expressions;
using GameStore.Domain.BusinessObjects;
using System.Data.Objects.SqlClient;
namespace GameStore.Services.ServicesImplementation.FilterImplementation.OrderFilter
{
    public class PeriodDateFilter : BaseFilter<Order>
    {
        private readonly TimeSpan? _date;
        private readonly DateTimeIntervalFlag _dateTimeIntervalFlag;

        public PeriodDateFilter(TimeSpan? date, DateTimeIntervalFlag dateTimeIntervalFlag)
        {
            _date = date;
            _dateTimeIntervalFlag = dateTimeIntervalFlag;
        }

        public override Expression<Func<Order, bool>> Execute(Expression<Func<Order, bool>> input)
        {
            if (_date != null)
            {
                Expression<Func<Order, bool>> filter;
                var comparer = DateTime.UtcNow - _date;

                if (_dateTimeIntervalFlag == DateTimeIntervalFlag.BeforeDateTime)
                {
                    filter = x => x.OrderDate < comparer;
                }
                else
                {
                    filter = x => x.OrderDate >= comparer;
                }

                return AggregateExpression(input, filter);
            }

            return input;
        }
    }
}
