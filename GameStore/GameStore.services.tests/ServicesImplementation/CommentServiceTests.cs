using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GameStore.DataAccess.Interfaces;
using GameStore.DataAccess.MSSQL.Entities;
using GameStore.DataAccess.UnitOfWork;
using GameStore.Domain.BusinessObjects;
using GameStore.Logging.Loggers;
using GameStore.Services.ServicesImplementation;
using Moq;
using NUnit.Framework;

namespace GameStore.Services.Tests.ServicesImplementation
{
    [TestFixture]
    class CommentServiceTests
    {
        private CommentService _sut;
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IGenericDataRepository<CommentEntity, Comment>> _commentRepository;
        private Mock<IMongoLogger<Comment>> _logger;
        private Comment _commentStub = new Comment();
        private static int CommentId = 1;
        private IList<Comment> _comments = new List<Comment>()
        {
            new Comment()
            {
                Id = "1",
                Name = "Author1",
                Body = "comment from Author1",
                GameId = "1",
                ParentCommentId = null
            },
            new Comment()
            {
                Id = "2",
                Name = "Author2",
                Body = "comment from Author2",
                GameId = "1",
                ParentCommentId = null
            },
            new Comment()
            {
                Id = "3",
                Name = "Author3",
                Body = "comment from Author3",
                GameId = "1",
                ParentCommentId = "2"
            },
            new Comment()
            {
                Id = "4",
                Name = "Author4",
                Body = "comment from Author4",
                GameId = "1",
                ParentCommentId = "2"
            },
            new Comment()
            {
                Id = "5",
                Name = "Author5",
                Body = "comment from Author5",
                GameId = "1",
                ParentCommentId = "3"
            },
            new Comment()
            {
                Id = "6",
                Name = "Author6",
                Body = "comment from Author6",
                GameId = "1",
                ParentCommentId = "5"
            }

        };

        [SetUp]
        public void Setup()
        {
            _commentRepository = new Mock<IGenericDataRepository<CommentEntity, Comment>>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _logger = new Mock<IMongoLogger<Comment>>();
            
            _sut = new CommentService(_unitOfWork.Object, _commentRepository.Object, _logger.Object);
        }

        [Test]
        public void Add_IsCalled_CalledOneTime()
        {
            _commentRepository.Setup(p => p.Add(It.IsAny<Comment>()));

            _sut.Add(new Comment());

            _commentRepository.Verify(u => u.Add(It.IsAny<Comment>()), Times.Once);
        }

        [Test]
        public void GetAllCommentsByGameKey_IsCalled_CalledOneTime()
        {
            _commentRepository.Setup(g => g.Get(It.IsAny<Expression<Func<Comment, bool>>>()))
                .Returns(() => new List<Comment>());

            _sut.GetAllCommentsByGameKey("game");

            _commentRepository.Verify(u => u.Get(It.IsAny<Expression<Func<Comment, bool>>>()), Times.Once);
        }

        [Test]
        public void RemoteCommentId_IsCalled_CalledOneTime()
        {
            _commentRepository.Setup(g => g.Remove(It.IsAny<string>()));

            _sut.Remove("133");

            _commentRepository.Verify(u => u.Remove(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void RemoteComment_IsCalled_CalledOneTime()
        {
            _commentRepository.Setup(g => g.Remove(It.IsAny<Comment>()));

            _sut.Remove(_commentStub);

            _commentRepository.Verify(u => u.Remove(It.IsAny<Comment>()), Times.Once);
        }

        [Test]
        public void UpdateComment_IsCalled_CalledOneTime()
        {
            _commentRepository.Setup(g => g.Update(It.IsAny<Comment>()));

            _sut.Update(_commentStub);

            _commentRepository.Verify(u => u.Update(It.IsAny<Comment>()), Times.Once);
        }

        [Test]
        public void GetStructureOfComments_StructureListOfComment_StructuredListOfComments()
        {
            _commentRepository.Setup(x => x.GetItemById(It.IsAny<string>())).Returns<string>(x => It.IsAny<Comment>());

            var result = _sut.GetStructureOfComments(_comments);

            Assert.AreSame(_comments[4], result.ElementAt(1).Comments.ElementAt(0).Comments.ElementAt(0));
        }

        [Test]
        public void GetStructureOfComments_SendNullToMethod_GetNull()
        {
            _commentRepository.Setup(g => g.GetItemById(It.IsAny<string>())).Returns<Comment>(x => It.IsAny<Comment>());

            var result = _sut.GetStructureOfComments(null);

            Assert.AreSame(null, result);
        }
    }
}
