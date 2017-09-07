using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Services.ServicesImplementation.FilterImplementation
{
    public abstract class BaseFilter<TSource>
    {
        public abstract Expression<Func<TSource, bool>> Execute(Expression<Func<TSource, bool>> input);

        protected Expression<Func<TSource, bool>> AggregateExpression(Expression<Func<TSource, bool>> mainExpression,
            Expression<Func<TSource, bool>> newExpression)
        {
            var visitor = new SwapVisitor(mainExpression.Parameters[0], newExpression.Parameters[0]);
            var result = Expression.Lambda<Func<TSource, bool>>(Expression.AndAlso(visitor.Visit(mainExpression.Body), newExpression.Body),
                newExpression.Parameters);
            return result;
        }

    }
}
