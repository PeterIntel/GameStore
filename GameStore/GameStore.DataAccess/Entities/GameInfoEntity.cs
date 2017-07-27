using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DataAccess.Entities
{
    public class GameInfoEntity : BasicEntity
    {
        public int? CountOfViews { set; get; }
        public DateTime UploadDate { set; get; }

        public virtual GameEntity Game { set; get; }
    }
}
