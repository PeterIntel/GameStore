using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.BusinessObjects
{
    class Publisher
    {
        public int Id { set; get; }
        public string CompanyName { set; get; }
        public string Description { set; get; }
        public string HomePage { set; get; }
    }
}
