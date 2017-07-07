using System;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.Domain.BusinessObjects
{
    public class Game : BasicDomainEntity
    {
        public int Id { set; get; }
        public string Key { set; get; }
        public string Description { set; get; }

        public IList<Comment> Comments { set; get; }
        public IList<Genre> Genres { set; get; }
        public IList<PlatformType> PlatformTypes { set; get; }
    }
}
