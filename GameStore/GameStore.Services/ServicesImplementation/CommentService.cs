﻿using System;
using System.Collections.Generic;
using System.Linq;
using GameStore.DataAccess.Interfaces;
using GameStore.DataAccess.MSSQL.Entities;
using GameStore.DataAccess.UnitOfWork;
using GameStore.Domain.BusinessObjects;
using GameStore.Domain.ServicesInterfaces;
using GameStore.Logging.Loggers;
using GameStore.Services.Localization;

namespace GameStore.Services.ServicesImplementation
{
    public class CommentService : BasicService<CommentEntity, Comment>, ICommentService
    {
        private readonly IGenericDataRepository<CommentEntity, Comment> _commentRepository;
        public CommentService(IUnitOfWork unitOfWork, IGenericDataRepository<CommentEntity, Comment> commentRepository, IMongoLogger<Comment> logger,
            ILocalizationProvider<Comment> localizatorProvider) :
            base(commentRepository, unitOfWork, logger, localizatorProvider)
        {
            _commentRepository = commentRepository;
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
    }
}
