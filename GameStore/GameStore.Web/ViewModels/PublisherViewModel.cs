using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Web.ViewModels
{
    public class PublisherViewModel
    {
        public string Id { set; get; }

        [Required]
        public string CompanyName { set; get; }

        [Required]
        public string Description { set; get; }

        [Required]
        public string HomePage { set; get; }

        public bool IsChecked { set; get; }

        public IList<GameViewModel> Games { set; get; }
    }
}