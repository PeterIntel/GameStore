using System;

namespace GameStore.Web.ViewModels
{
    public class GameInfoViewModel
    {
        public int? CountOfViews { set; get; }

        public DateTime UploadDate { set; get; }

        public GameViewModel Game { set; get; }
    }
}