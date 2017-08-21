using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GameStore.DataAccess.Decorators;
using GameStore.DataAccess.Interfaces;
using GameStore.DataAccess.MSSQL.Entities;
using GameStore.DataAccess.UnitOfWork;
using GameStore.Domain.BusinessObjects;
using GameStore.Domain.ServicesInterfaces;
using GameStore.Logging.Loggers;

namespace GameStore.Services.ServicesImplementation
{
    public class CommentService : BasicService<Comment>, ICommentService
    {
        private IUnitOfWork _unitOfWork;
        private IGenericDataRepository<CommentEntity, Comment> _commentRepository;
        private IMongoLogger<Comment> _logger;
        public CommentService(IUnitOfWork unitOfWork, IGenericDataRepository<CommentEntity, Comment> commentRepository, IMongoLogger<Comment> logger)
        {
            _unitOfWork = unitOfWork;
            _commentRepository = commentRepository;
            _logger = logger;
        }
        public void Add(Comment item)
        {
            AssignIdIfEmpty(item);
            _commentRepository.Add(item);
            _unitOfWork.Save();
            _logger.Write(Operation.Insert, item);
        }

        public IEnumerable<Comment> GetAllCommentsByGameKey(string gameKey)
        {
            return _commentRepository.Get(x => x.Game.Key == gameKey).ToList();
        }

        public IEnumerable<Comment> GetStructureOfComments(IEnumerable<Comment> comments)
        {
            IList<Comment> roots = null;
            if (comments != null && comments.Any())
            {
                var groups = comments.GroupBy(x => x.ParentCommentId).ToList();

                roots = groups.FirstOrDefault(x => string.IsNullOrEmpty(x.Key)).ToList();

                if (roots.Count > 0)
                {
                    var dict = groups.Where(x => String.IsNullOrEmpty(x.Key) == false).ToDictionary(x => x.Key, x => x.ToList());
                    foreach (var x in roots)
                    {
                        AddChildren(x, dict);
                    }
                }
            }
            return roots;
        }

        private void AddChildren(Comment node, IDictionary<string, List<Comment>> source)
        {
            if (node.ParentCommentId != null)
            {
                node.ParentComment = _commentRepository.GetItemById(node.ParentCommentId);
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
            _commentRepository.Remove(id);
            _unitOfWork.Save();
        }

        public void Remove(Comment item)
        {
            _commentRepository.Remove(item);
            _unitOfWork.Save();
            _logger.Write(Operation.Delete, item);
        }

        public void Update(Comment item)
        {
            _commentRepository.Update(item);
            _unitOfWork.Save();
            var updatedComment = _commentRepository.GetItemById(item.Id);
            _logger.Write(Operation.Update, item, updatedComment);
        }
        public IEnumerable<Comment> Get(params Expression<Func<Comment, object>>[] includeProperties)
        {
            return _commentRepository.Get(includeProperties);
        }

        public bool Any(Expression<Func<Comment, bool>> filter)
        {
            return _commentRepository.Any(filter);
        }
    }
}
