using System;
using System.Linq.Expressions;
using GameStore.Domain.BusinessObjects;

namespace GameStore.Services.ServicesImplementation.FilterImplementation.GameFilters
{
    public class PublishedDateFilter : BaseFilter<Game>
    {
        private readonly DateTimeIntervals _dateTimeIntervalsFilter;

        public PublishedDateFilter(DateTimeIntervals dateTimeIntervals)
        {
            _dateTimeIntervalsFilter = dateTimeIntervals;
        }
        public override Expression<Func<Game, bool>> Execute(Expression<Func<Game, bool>> input)
        {
            if (_dateTimeIntervalsFilter != DateTimeIntervals.AllTime)
            {
                DateTime dateTimeInterval = GetDateTimeFilter(_dateTimeIntervalsFilter);
                Expression<Func<Game, bool>> filter = x => x.PublishedDate > dateTimeInterval;
                return AggregateExpression(input, filter);
            }

            return input;
        }

        private DateTime GetDateTimeFilter(DateTimeIntervals dateFilter)
        {
            switch (dateFilter)
            {
                case DateTimeIntervals.LastWeek:
                    return DateTime.UtcNow.AddDays(-7);
                case DateTimeIntervals.LastMonth:
                    return DateTime.UtcNow.AddMonths(-1);
                case DateTimeIntervals.LastYear:
                    return DateTime.UtcNow.AddYears(-1);
                case DateTimeIntervals.TwoYears:
                    return DateTime.UtcNow.AddYears(-2);
                case DateTimeIntervals.ThreeYears:
                    return DateTime.UtcNow.AddYears(-3);
                default:
                    return new DateTime();
            }
        }
    }
}
