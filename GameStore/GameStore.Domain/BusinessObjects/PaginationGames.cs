using System.Collections.Generic;

namespace GameStore.Domain.BusinessObjects
{
    public class PaginationGames
    {
        public int Count { set; get; }

        public IEnumerable<Game> Games { set; get; }
    }
}
