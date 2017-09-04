using System.Collections.Generic;
using GameStore.Domain.BusinessObjects.LocalizationObjects;
using Newtonsoft.Json;

namespace GameStore.Domain.BusinessObjects
{
    public class Publisher : BasicDomain
    {
        public string CompanyName { set; get; }
        public string Description { set; get; }
        public string HomePage { set; get; }
        public bool IsChecked { set; get; }
        [JsonIgnore]
        public virtual IEnumerable<Game> Games { set; get; }
        [JsonIgnore]
        public IEnumerable<PublisherLocal> Locals { get; set; }
    }
}
