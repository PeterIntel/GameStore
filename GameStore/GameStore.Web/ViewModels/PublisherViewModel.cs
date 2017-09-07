using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GameStore.Web.App_LocalResources;

namespace GameStore.Web.ViewModels
{
    public class PublisherViewModel
    {
        public string Id { set; get; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "Company", ResourceType = typeof(Resources))]
        public string CompanyName { set; get; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "Description", ResourceType = typeof(Resources))]
        public string Description { set; get; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "HomePage", ResourceType = typeof(Resources))]
        public string HomePage { set; get; }
        public bool IsChecked { set; get; }
        public bool IsSqlEntity { set; get; }
        public IList<GameViewModel> Games { set; get; }
    }
}