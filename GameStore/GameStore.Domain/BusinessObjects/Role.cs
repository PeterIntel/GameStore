using System.Collections.Generic;

namespace GameStore.Domain.BusinessObjects
{
    public class Role : BasicDomain
    {
        public RoleEnum RoleEnum { set; get; }
        public IEnumerable<User> Users { set; get; }
        public bool IsChecked { set; get; }
    }
}
