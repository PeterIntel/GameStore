using System;
using System.Linq.Expressions;
using GameStore.Domain.BusinessObjects;

namespace GameStore.Services.ServicesImplementation.FilterImplementation.GameFilters
{
    public class GameNameFilter : BaseFilter<Game>
    {
        private readonly string _partGameName;

        public GameNameFilter(string partGameName)
        {
            _partGameName = partGameName;
        }
        public override Expression<Func<Game, bool>> Execute(Expression<Func<Game, bool>> input)
        {
            if (_partGameName != null)
            {
                Expression<Func<Game, bool>> filter = x => x.Key.Contains(_partGameName);
                return AggregateExpression(input, filter);
            }
            return input;
        }
    }
}
