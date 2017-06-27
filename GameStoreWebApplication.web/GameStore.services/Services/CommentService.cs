using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDataAccessLayer.DAL.UnitOfWork;
using DomainLayer.contracts.DomainModels;
using System.Linq.Expressions;

namespace GameStore.services.Services
{
    public class CommentService : ICommentService
    {
        private IUnitOfWork _unitOfWork;
        public CommentService()
        {
            _unitOfWork = new UnitOfWork();
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
