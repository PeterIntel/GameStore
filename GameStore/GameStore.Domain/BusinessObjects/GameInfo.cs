using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.BusinessObjects
{
    public class GameInfo
    {
        public string Id { set; get; }
        public int? CountOfViews { set; get; }
        public DateTime UploadDate { set; get; }

        public Game Game { set; get; }
    }
}
