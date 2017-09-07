using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GameStore.Web.App_LocalResources;

namespace GameStore.Web.ViewModels
{
    public class GenreViewModel
    {
        public string Id { set; get; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources))]
        public string Name { set; get; }
        public string ParentGenreId { set; get; }
        public string ParentGenreName { set; get; }
        public bool IsSqlEntity { set; get; }
        public bool IsChecked { set; get; }
        public IList<GenreViewModel> Genres { set; get; }
        public IList<GameViewModel> Games { set; get; }
    }
}
