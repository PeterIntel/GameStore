using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Domain.BusinessObjects;

namespace GameStore.Domain.ServicesInterfaces
{
    public interface ICommentService : ICrudService<Comment>
    {
        IEnumerable<Comment> GetAllCommentsByGameKey(string gameKey);
        IEnumerable<Comment> GetStructureOfComments(IEnumerable<Comment> comments);
    }
}
