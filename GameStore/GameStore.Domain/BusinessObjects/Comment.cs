﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GameStore.Domain.BusinessObjects
{
    public class Comment : BasicDomain
    {
        public string Name { set; get; }
        public string Body { set; get; }
        public string ParentCommentId { set; get; }
        public string GameId { set; get; }
        [JsonIgnore]
        public Game Game { set; get; }
        public Comment ParentComment { set; get; }
        [JsonIgnore]
        public IEnumerable<Comment> Comments { set; get; }
    }
}
