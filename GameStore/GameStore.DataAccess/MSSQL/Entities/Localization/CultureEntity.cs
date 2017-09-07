using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DataAccess.MSSQL.Entities.Localization
{
    public class CultureEntity : BasicEntity
    {
        public string Title { get; set; }

        public string Code { get; set; }
    }
}
