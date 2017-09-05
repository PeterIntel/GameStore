using System.Collections.Generic;
using GameStore.Domain.BusinessObjects.LocalizationObjects;
using Newtonsoft.Json;

namespace GameStore.Domain.BusinessObjects
{
    public class Genre : BasicDomain
    {
        public string Name { set; get; }
        public string ParentGenreId { set; get; }
        public string ParentGenreName { set; get; }
        public bool IsChecked { set; get; }
        public Genre ParentGenre { set; get; }
        public IEnumerable<Genre> Genres { set; get; }
        [JsonIgnore]
        public IEnumerable<Game> Games { set; get; }
        [JsonIgnore]
        public IEnumerable<GenreLocal> Locals { get; set; } = new List<GenreLocal>();
    }
}
