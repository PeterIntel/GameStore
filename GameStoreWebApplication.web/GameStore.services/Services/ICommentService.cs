using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.contracts.DomainModels;

namespace GameStore.services.Services
{
    public interface ICommentService : IService<Comment>
    {
        IList<Comment> GetAllCommentsByGameKey(string gameKey);
    }
}
