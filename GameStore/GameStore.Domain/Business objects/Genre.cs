﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Domain.BusinessObjects
{
    public class Genre : BasicDomainEntity 
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public int? ParentGenreId { set; get; }
        public IList<Genre> Genres { set; get; }
        public virtual IList<Game> Games { set; get; }
    }
}