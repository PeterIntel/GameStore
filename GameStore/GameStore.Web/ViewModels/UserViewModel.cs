using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GameStore.Web.App_LocalResources;
using GameStore.Web.Attributes;

namespace GameStore.Web.ViewModels
{
    public class UserViewModel
    {
        public string Id { set; get; }
        [Required(ErrorMessageResourceName = "LoginFieldRequired", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "LoginName", ResourceType = typeof(Resources))]
        public string Login { set; get; }
        [Required(ErrorMessageResourceName = "FirstNameRequired", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "FirstName", ResourceType = typeof(Resources))]
        public string FirstName { set; get; }
        [Required(ErrorMessageResourceName = "LastNameRequired", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "LastName", ResourceType = typeof(Resources))]
        public string LastName { set; get; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "BirthDay", ResourceType = typeof(Resources))]
        [ValidateBirthDate]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? BirthDay { set; get; }
        [Required(ErrorMessageResourceName = "EmailRequired", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "Email", ResourceType = typeof(Resources))]
        [EmailAddress(ErrorMessageResourceName = "ErrorEmail", ErrorMessageResourceType = typeof(Resources))]
        public string Email { set; get; }
        [Required(ErrorMessageResourceName = "PasswordRequired", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "Password", ResourceType = typeof(Resources))]
        [StringLength(10, ErrorMessageResourceName = "ErrorPassword", ErrorMessageResourceType = typeof(Resources), MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { set; get; }
        [Required(ErrorMessageResourceName = "ConfirmationRequired", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "PasswordConfirmation", ResourceType = typeof(Resources))]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessageResourceName = "ErrorPasswordCompare", ErrorMessageResourceType = typeof(Resources))]
        public string ConfirmPassword { set; get; }
        [Display(Name = "Roles", ResourceType = typeof(Resources))]
        public IList<RoleViewModel> Roles { set; get; }
        public IList<string> IdRoles { set; get; }
        [Display(Name = "Publisher", ResourceType = typeof(Resources))]
        public string SelectedPublisher { set; get; }
        public PublisherViewModel Publisher { set; get; }
        public IList<PublisherViewModel> Publishers { set; get; }
    }
}