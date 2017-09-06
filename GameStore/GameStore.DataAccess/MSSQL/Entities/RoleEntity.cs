using System.Collections.Generic;
using GameStore.Domain.BusinessObjects;

namespace GameStore.DataAccess.MSSQL.Entities
{
    public class RoleEntity : BasicEntity
    {
        public RoleEnum Role { set; get; }

        public virtual IList<UserEntity> Users { set; get; }
    }

}
