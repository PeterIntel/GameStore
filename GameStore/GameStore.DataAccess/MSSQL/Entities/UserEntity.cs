using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DataAccess.MSSQL.Entities
{
    public class UserEntity : BasicEntity
    {
        public string Login { set; get; }
        public string Email { set; get; }
        public string Password { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public DateTime? BirthDay { set; get; }
        public virtual IList<RoleEntity> Roles { set; get; }
        public virtual IList<OrderEntity> Orders { set; get; }
        public virtual PublisherEntity Publisher { set; get; }
    }

}
