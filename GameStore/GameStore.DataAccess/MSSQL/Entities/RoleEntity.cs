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
        public RoleEnum Role { set; get; }
        public virtual IList<UserEntity> Users { set; get; }
    }

}
