using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.DataAccess.UnitOfWork;
using GameStore.Domain.BusinessObjects;
using GameStore.Domain.ServicesInterfaces;

namespace GameStore.Services.ServicesImplementation
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
            _unitOfWork.CommentRepository.Add(item);
            _unitOfWork.Save();
        }

        public IEnumerable<Comment> GetAllCommentsByGameKey(string gameKey)
        {
            return _unitOfWork.CommentRepository.GetAll(x => x.Game.Key == gameKey);
        }

        public void Remove(int id)
        {
            _unitOfWork.CommentRepository.Remove(id);
            _unitOfWork.Save();
        }

        public void Remove(Comment item)
        {
            _unitOfWork.CommentRepository.Remove(item);
            _unitOfWork.Save();
        }

        public void Update(Comment item)
        {
            _unitOfWork.CommentRepository.Update(item);
            _unitOfWork.Save();
        }
    }
}
