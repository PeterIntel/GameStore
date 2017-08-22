using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GameStore.Domain.BusinessObjects;
using GameStore.Web.Attributes;

namespace GameStore.Web.ViewModels
{
    public class UserViewModel
    {
        [Required]
        [Display(Name = "Login")]
        public string Login { set; get; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { set; get; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { set; get; }
        [Required]
        [Display(Name = "Birth Date")]
        [ValidateBirthDate]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { set; get; }
        [Required]
        [Display(Name = "E-mail")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { set; get; }
        [Required]
        [Display(Name = "Password")]
        [StringLength(10, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { set; get; }
        [Required]
        [Display(Name = "Comfirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirm password do not match.")]
        public string ConfirmPassword { set; get; }
        public IList<RoleViewModel> Roles { set; get; }
        public IList<string> IdRoles { set; get; }
    }
}