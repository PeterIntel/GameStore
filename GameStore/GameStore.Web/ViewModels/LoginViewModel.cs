using System.ComponentModel.DataAnnotations;

namespace GameStore.Web.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "User Name")]
        public string UserName { set; get; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { set; get; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { set; get; }
    }
}