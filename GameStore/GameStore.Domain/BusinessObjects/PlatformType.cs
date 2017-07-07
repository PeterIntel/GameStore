using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Domain.BusinessObjects
{
    public class PlatformType : BasicDomainEntity
    {
        public int Id { set; get; }
        public string TypeName { set; get; }
        public IList<Game> Games { set; get; }
    }
}
