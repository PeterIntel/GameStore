using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.BusinessObjects
{
    public class PaginationGames
    {
        public int Count { set; get; }
        public IEnumerable<Game> Games { set; get; }
    }
}
