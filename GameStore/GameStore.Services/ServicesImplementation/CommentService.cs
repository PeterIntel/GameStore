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

        public IEnumerable<Comment> GetStructureOfComments(IEnumerable<Comment> comments)
        {
            if (comments.Any())
            {
                var groups = comments.GroupBy(x => x.ParentCommentId).ToList();

                var roots = groups.FirstOrDefault(x => x.Key.HasValue == false).ToList();
                if (roots.Count > 0)
                {
                    var dict = groups.Where(x => x.Key.HasValue).ToDictionary(x => x.Key.Value, x => x.ToList());
                    foreach (var x in roots)
                    {
                        AddChildren(x, dict);
                    }
                }
            }
            return comments;
        }

        private void AddChildren(Comment node, IDictionary<int,  List<Comment>> source)
        {
            if (source.ContainsKey(node.Id))
            {
                node.Comments = source[node.Id];
                foreach (var x in node.Comments)
                {
                    AddChildren(x, source);
                }
            }
            else
            {
                node.Comments = new List<Comment>();
            }
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
