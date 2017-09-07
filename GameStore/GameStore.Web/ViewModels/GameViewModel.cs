using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GameStore.Web.App_LocalResources;

namespace GameStore.Web.ViewModels
{
    public class GameViewModel
    {
        public string Id { set; get; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "GameKey", ResourceType = typeof(Resources))]
        public string Key { set; get; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "Description", ResourceType = typeof(Resources))]
        public string Description { set; get; }
        [Display(Name = "Price", ResourceType = typeof(Resources))]
        public decimal Price { set; get; }
        [Display(Name = "UnitsInStock", ResourceType = typeof(Resources))]
        public short UnitsInStock { set; get; }
        [Display(Name = "Discontinued", ResourceType = typeof(Resources))]
        public bool Discontinued { set; get; }
        public bool IsDeleted { set; get; }
        [Display(Name = "Publisher", ResourceType = typeof(Resources))]
        public string SelectedPublisher { set; get; }
        [Display(Name = "PublicationDate", ResourceType = typeof(Resources))]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? PublishedDate { set; get; }
        public PublisherViewModel Publisher { set; get; }
        public IList<PublisherViewModel> Publishers { set; get; }
        public IList<CommentViewModel> Comments { set; get; }
        [Display(Name = "Genres", ResourceType = typeof(Resources))]
        public IList<GenreViewModel> Genres { set; get; }
        public IEnumerable<string> NameGenres { set; get; }
        [Display(Name = "PlatformTypes", ResourceType = typeof(Resources))]
        public IList<PlatformTypeViewModel> PlatformTypes { set; get; }
        public IList<string> NamePlatformtypes { set; get; }
        public GameInfoViewModel GameInfo { set; get; }
    }
}
