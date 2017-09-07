using System;
using System.ComponentModel.DataAnnotations;
using GameStore.Web.App_LocalResources;

namespace GameStore.Web.ViewModels
{
    public class GameInfoViewModel
    {
        [Display(Name = "ViewCount", ResourceType = typeof(Resources))]
        public int? CountOfViews { set; get; }
        [Display(Name = "AddedToStoreDate", ResourceType = typeof(Resources))]
        public DateTime UploadDate { set; get; }
        public GameViewModel Game { set; get; }
    }
}