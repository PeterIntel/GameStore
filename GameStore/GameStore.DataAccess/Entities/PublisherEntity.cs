using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DataAccess.Entities
{
    public class PublisherEntity : BasicEntity
    {
        public int Id { set; get; }
        public string CompanyName { set; get; }
        public string Description { set; get; }
        public string HomePage { set; get; }
    }
}
