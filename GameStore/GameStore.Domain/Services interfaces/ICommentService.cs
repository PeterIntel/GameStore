﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Domain.BusinessObjects;

namespace GameStore.Domain.Services_interfaces
{
    public interface ICommentService : ICrudService<Comment>
    {
        IEnumerable<Comment> GetAllCommentsByGameKey(string gameKey);
    }
}