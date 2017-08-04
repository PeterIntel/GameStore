using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            return _unitOfWork.CommentRepository.Get(x => x.Game.Key == gameKey);
        }

        public IEnumerable<Comment> GetStructureOfComments(IEnumerable<Comment> comments)
        {
            IList<Comment> roots = null;
            if (comments != null && comments.Any())
            {
                var groups = comments.GroupBy(x => x.ParentCommentId).ToList();
                IList<Dictionary<int, Comment>> a; Dictionary<string, Comment > b;

                    roots = groups.FirstOrDefault(x => String.IsNullOrEmpty(x.Key) == false).ToList();

                if (roots.Count > 0)
                {
                    var dict = groups.Where(x => String.IsNullOrEmpty(x.Key)).ToDictionary(x => x.Key, x => x.ToList());
                    foreach (var x in roots)
                    {
                        AddChildren(x, dict);
                    }
                }
            }
            return roots;
        }

        private void AddChildren(Comment node, IDictionary<string,  List<Comment>> source)
        {
            if (node.ParentCommentId != null)
            {
                node.ParentComment = _unitOfWork.CommentRepository.GetItemById(node.ParentCommentId);
            }
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

        public void Remove(string id)
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
        public IEnumerable<Comment> Get(params Expression<Func<Comment, object>>[] includeProperties)
        {
            return _unitOfWork.CommentRepository.Get(includeProperties);
        }
    }
}
