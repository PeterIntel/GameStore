﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Domain.BusinessObjects
{
    public class PlatformType
    {
        public int Id { set; get; }
        public string TypeName { set; get; }
        public IEnumerable<Game> Games { set; get; }
    }
}
