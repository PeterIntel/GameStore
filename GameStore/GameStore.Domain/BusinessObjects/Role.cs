using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.BusinessObjects
{
    public class Role : BasicDomain
    {
        public RoleEnum RoleEnum { set; get; }
        public IEnumerable<User> Users { set; get; }
        public bool IsChecked { set; get; }
    }
}
