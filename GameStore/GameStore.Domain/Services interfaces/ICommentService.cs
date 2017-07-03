using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Domain.Business_objects;

namespace GameStore.Domain.Services_interfaces
{
    public interface ICommentService : ICrudService<Comment>
    {
        IList<Comment> GetAllCommentsByGameKey(string gameKey);
    }
}
