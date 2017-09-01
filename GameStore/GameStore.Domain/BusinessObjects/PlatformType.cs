using System.Collections.Generic;
using Newtonsoft.Json;

namespace GameStore.Domain.BusinessObjects
{
    public class PlatformType : BasicDomain
    {
        public string TypeName { set; get; }
        public bool IsChecked { set; get; }
        [JsonIgnore]
        public IEnumerable<Game> Games { set; get; }
    }
}
