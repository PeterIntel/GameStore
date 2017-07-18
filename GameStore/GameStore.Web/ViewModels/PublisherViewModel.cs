using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStore.Web.ViewModels
{
    public class PublisherViewModel
    {
        public int Id { set; get; }
        public string CompanyName { set; get; }
        public string Description { set; get; }
        public string HomePage { set; get; }
    }
}