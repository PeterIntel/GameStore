using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
