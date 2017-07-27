using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GameStore.Domain.BusinessObjects;

namespace GameStore.Services.ServicesImplementation.FilterImplementation
{
    public class PublisherFilter : BaseFilter<Game>
    {
        private readonly IEnumerable<string> _publishers;

        public PublisherFilter(IEnumerable<string> publishers)
        {
            _publishers = publishers;
        }
        public override Expression<Func<Game, bool>> Execute(Expression<Func<Game, bool>> input)
        {
            Expression<Func<Game, bool>> filter = x => _publishers.Contains(x.Publisher.CompanyName);
            return AggregateExpression(input, filter);
        }
    }
}
