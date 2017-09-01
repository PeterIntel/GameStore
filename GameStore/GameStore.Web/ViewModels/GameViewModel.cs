﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Web.ViewModels
{
    public class GameViewModel
    {
        public string Id { set; get; }
        [Required]
        public string Key { set; get; }
        [Required]
        public string Description { set; get; }
        public decimal Price { set; get; }
        public short UnitsInStock { set; get; }
        public bool Discontinued { set; get; }
        public bool IsDeleted { set; get; }
        [Display(Name = "Publisher")]
        public string SelectedPublisher { set; get; }
        public DateTime? PublishedDate { set; get; }
        public PublisherViewModel Publisher { set; get; }
        public IList<PublisherViewModel> Publishers { set; get; }
        public IList<CommentViewModel> Comments { set; get; }
        public IList<GenreViewModel> Genres { set; get; }
        public IEnumerable<string> NameGenres { set; get; }
        public IList<PlatformTypeViewModel> PlatformTypes { set; get; }
        public IList<string> NamePlatformtypes { set; get; }
        public GameInfoViewModel GameInfo { set; get; }
    }
}
