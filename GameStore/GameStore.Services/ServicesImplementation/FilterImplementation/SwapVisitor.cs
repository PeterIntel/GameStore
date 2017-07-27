using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Services.ServicesImplementation.FilterImplementation
{
    public class SwapVisitor: ExpressionVisitor
    {
        private readonly Expression _expressionFrom;
        private readonly Expression _expressionTo;

        public SwapVisitor(Expression expressionFrom, Expression expressionTo)
        {
            _expressionFrom = expressionFrom;
            _expressionTo = expressionTo;
        }

        public override Expression Visit(Expression node)
        {
            var result = (node == _expressionFrom ? _expressionTo : base.Visit(node));
            return result;
        }
    }
}
