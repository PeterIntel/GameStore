using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.DataAccess.UnitOfWork;
using GameStore.Domain.Business_objects;
using GameStore.Domain.Services_interfaces;

namespace GameStore.Services.Services_implementation
{
    public class CommentService : ICommentService
    {
        private IUnitOfWork _unitOfWork;
        public CommentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Add(Comment item)
        {
            _unitOfWork.Comments.Add(item);
            _unitOfWork.Save();
        }

        public IList<Comment> GetAllCommentsByGameKey(string gameKey)
        {
            return _unitOfWork.Comments.GetAll(x => x.Game.Key == gameKey);
        }

        public void Remove(int id)
        {
            _unitOfWork.Comments.Remove(id);
            _unitOfWork.Save();
        }

        public void Remove(Comment item)
        {
            _unitOfWork.Comments.Remove(item);
            _unitOfWork.Save();
        }

        public void Update(Comment item)
        {
            _unitOfWork.Comments.Update(item);
            _unitOfWork.Save();
        }
    }
}
