using System;
using System.Collections.Generic;

namespace GameStore.Domain.BusinessObjects
{
    public class Game
    {
        public string Id { set; get; }
        public string Key { set; get; }
        public string Description { set; get; }
        public decimal Price { set; get; }
        public short UnitsInStock { set; get; }
        public bool Discontinued { set; get; }
        public DateTime? PublishedDate { set; get; }
        public GameInfo GameInfo { set; get; }
        public Publisher Publisher { set; get; }
        public IEnumerable<Comment> Comments { set; get; }
        public IEnumerable<Genre> Genres { set; get; }
        public IEnumerable<string> NameGenres { set; get; }
        public IEnumerable<PlatformType> PlatformTypes { set; get; }
        public IEnumerable<string> NamePlatformTypes { set; get; }
    }
}
