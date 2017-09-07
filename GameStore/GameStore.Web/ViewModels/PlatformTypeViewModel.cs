using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GameStore.Web.App_LocalResources;

namespace GameStore.Web.ViewModels
{
    public class PlatformTypeViewModel
    {
        public string Id { set; get; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources))]
        public string TypeName { set; get; }

        public bool IsChecked { set; get; }

        public IList<GameViewModel> Games { set; get; }
    }
}
