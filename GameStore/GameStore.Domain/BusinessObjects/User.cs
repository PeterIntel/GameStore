using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.BusinessObjects
{
    public class User : BasicDomain
    {
        public string Login { set; get; }
        public string Email { set; get; }
        public string Password { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public DateTime? BirthDay { set; get; }
        public IEnumerable<Role> Roles { set; get; } = new List<Role>();
        public IEnumerable<string> IdRoles { set; get; }
        public IEnumerable<Order> Orders { set; get; } 

        public bool IsInRole(RoleEnum role)
        {
            return Roles.Select(x => x.RoleEnum).Contains(role);
        }
    }
}

