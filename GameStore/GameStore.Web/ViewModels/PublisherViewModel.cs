﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Web.ViewModels
{
    public class PublisherViewModel
    {
        public int Id { set; get; }
        [Required]
        public string CompanyName { set; get; }
        [Required]
        public string Description { set; get; }
        [Required]
        public string HomePage { set; get; }
        public IList<GameViewModel> Games { set; get; }
    }
}