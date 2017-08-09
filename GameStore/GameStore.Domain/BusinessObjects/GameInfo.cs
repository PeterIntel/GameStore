﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.BusinessObjects
{
    public class GameInfo : BasicDomain
    {
        public int? CountOfViews { set; get; }
        public DateTime UploadDate { set; get; }

        public Game Game { set; get; }
    }
}
