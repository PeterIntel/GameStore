using System.Collections.Generic;

namespace GameStore.Web.ViewModels
{
    public class PlatformTypeViewModel
    {
        public string TypeName { set; get; }

        public bool IsChecked { set; get; }

        public IList<GameViewModel> Games { set; get; }
    }
}
