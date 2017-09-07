using System.ComponentModel.DataAnnotations;
using GameStore.Web.App_LocalResources;

namespace GameStore.Web.ViewModels
{
    public class LoginViewModel
    {
        [Required( ErrorMessageResourceName = "LoginRequired", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "User", ResourceType = typeof(Resources))]
        public string UserName { set; get; }
        [Required(ErrorMessageResourceName = "PasswordRequired", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "Password", ResourceType = typeof(Resources))]
        [DataType(DataType.Password)]
        public string Password { set; get; }
        [Display(Name = "RememberMe", ResourceType = typeof(Resources))]
        public bool RememberMe { set; get; }
    }
}