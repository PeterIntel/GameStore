using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Linq;
using System.Web.Script.Serialization;

namespace GameStore.Domain.BusinessObjects
{
    public class Game
    {
        public int Id { set; get; }
        public string Key { set; get; }
        public string Description { set; get; }
        public decimal Price { set; get; }
        public short UnitsInStock { set; get; }
        public bool Discontinued { set; get; }
        public virtual Publisher Publisher { set; get; }
        [ScriptIgnore(ApplyToOverrides = true)]
        public IEnumerable<Comment> Comments { set; get; }
        [ScriptIgnore(ApplyToOverrides = true)]
        public IEnumerable<Genre> Genres { set; get; }
        public IEnumerable<string> NameGenres { set; get; }
        [ScriptIgnore(ApplyToOverrides = true)]
        public IEnumerable<PlatformType> PlatformTypes { set; get; }
        public IEnumerable<string> NamePlatformTypes { set; get; }
    }
}
