using System;
using Newtonsoft.Json;

namespace GameStore.Domain.BusinessObjects
{
    public class GameInfo : BasicDomain
    {
        public int? CountOfViews { set; get; }
        public DateTime UploadDate { set; get; }
        [JsonIgnore]
        public Game Game { set; get; }
    }
}
