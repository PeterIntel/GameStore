﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using GameStore.Domain.BusinessObjects;

namespace GameStore.Services.ServicesImplementation.FilterImplementation
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
                Expression<Func<Game, bool>> filter = x => x.PublishedDate > GetDateTimeFilter(_dateTimeIntervalsFilter);
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
                case DateTimeIntervals.TwoYear:
                    return DateTime.UtcNow.AddYears(-2);
                case DateTimeIntervals.TreeYear:
                    return DateTime.UtcNow.AddYears(-3);
                default:
                    return new DateTime();
            }
        }
    }
}
