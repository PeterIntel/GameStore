using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Domain.BusinessObjects;

namespace GameStore.DataAccess.MSSQL.Entities
{
    public class RoleEntity : BasicEntity
    {
        public Role Role { set; get; }
        public virtual IList<UserEntity> Users { set; get; }
    }

}
