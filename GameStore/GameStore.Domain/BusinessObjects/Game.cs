using System;
using System.Collections.Generic;

namespace GameStore.Domain.BusinessObjects
{
    public class Game : BasicDomain
    {
        public string Key { set; get; }
        public string Description { set; get; }
        public decimal Price { set; get; }
        public short UnitsInStock { set; get; }
        public bool Discontinued { set; get; }
        public DateTime? PublishedDate { set; get; } = new DateTime();
        public GameInfo GameInfo { set; get; } = new GameInfo();
        public Publisher Publisher { set; get; } = new Publisher();
        public IEnumerable<Comment> Comments { set; get; } = new List<Comment>();
        public IEnumerable<Genre> Genres { set; get; } 
        public IEnumerable<string> NameGenres { set; get; } = new List<string>();
        public IEnumerable<PlatformType> PlatformTypes { set; get; } = new List<PlatformType>();
        public IEnumerable<string> NamePlatformTypes { set; get; } = new List<string>();
    }
}
