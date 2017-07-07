using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Web.ViewModels
{
    public class PlatformTypeViewModel
    {
        public int Id { set; get; }
        public string TypeName { set; get; }
        public IList<GameViewModel> Games { set; get; }
    }
}
