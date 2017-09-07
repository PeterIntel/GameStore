using System;

namespace GameStore.DataAccess.MSSQL.Entities
{
    public class GameInfoEntity : BasicEntity
    {
        public int? CountOfViews { set; get; }

        public DateTime UploadDate { set; get; }


        public virtual GameEntity Game { set; get; }
    }
}
