using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
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
