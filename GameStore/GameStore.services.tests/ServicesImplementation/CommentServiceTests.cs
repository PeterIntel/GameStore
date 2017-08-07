using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GameStore.DataAccess.MSSQL.Entities;
using GameStore.DataAccess.MSSQL.Repositories;
using GameStore.DataAccess.UnitOfWork;
using GameStore.Domain.BusinessObjects;
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
        private Mock<IGenericDataRepository<CommentEntity, Comment>> _rep;
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
            _rep = new Mock<IGenericDataRepository<CommentEntity, Comment>>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _unitOfWork.Setup(m => m.CommentRepository).Returns(_rep.Object);
            
            _sut = new CommentService(_unitOfWork.Object);
        }

        [Test]
        public void Add_IsCalled_CalledOneTime()
        {
            _unitOfWork.Setup(p => p.CommentRepository.Add(It.IsAny<Comment>()));

            _sut.Add(new Comment());

            _unitOfWork.Verify(u => u.CommentRepository.Add(It.IsAny<Comment>()), Times.Once);
        }

        [Test]
        public void GetAllCommentsByGameKey_IsCalled_CalledOneTime()
        {
            _unitOfWork.Setup(g => g.CommentRepository.Get(It.IsAny<Expression<Func<Comment, bool>>>(), It.IsAny<Expression<Func<Comment, object>>[]>()))
                .Returns(() => It.IsAny<IList<Comment>>());

            _sut.GetAllCommentsByGameKey("game");

            _unitOfWork.Verify(u => u.CommentRepository.Get(It.IsAny<Expression<Func<Comment, bool>>>(), It.IsAny<Expression<Func<Comment, object>>[]>()), Times.Once);
        }

        [Test]
        public void RemoteCommentId_IsCalled_CalledOneTime()
        {
            _unitOfWork.Setup(g => g.CommentRepository.Remove(It.IsAny<string>()));

            _sut.Remove("133");

            _unitOfWork.Verify(u => u.CommentRepository.Remove(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void RemoteComment_IsCalled_CalledOneTime()
        {
            _unitOfWork.Setup(g => g.CommentRepository.Remove(It.IsAny<Comment>()));

            _sut.Remove(_commentStub);

            _unitOfWork.Verify(u => u.CommentRepository.Remove(It.IsAny<Comment>()), Times.Once);
        }

        [Test]
        public void UpdateComment_IsCalled_CalledOneTime()
        {
            _unitOfWork.Setup(g => g.CommentRepository.Update(It.IsAny<Comment>()));

            _sut.Update(_commentStub);

            _unitOfWork.Verify(u => u.CommentRepository.Update(It.IsAny<Comment>()), Times.Once);
        }

        [Test]
        public void GetStructureOfComments_StructureListOfComment_StructuredListOfComments()
        {
            _unitOfWork.Setup(x => x.CommentRepository.GetItemById(It.IsAny<string>())).Returns<int>(x => It.IsAny<Comment>());

            var result = _sut.GetStructureOfComments(_comments);

            Assert.AreSame(_comments[4], result.ElementAt(1).Comments.ElementAt(0).Comments.ElementAt(0));
        }

        [Test]
        public void GetStructureOfComments_SendNullToMethod_GetNull()
        {
            _unitOfWork.Setup(g => g.CommentRepository.GetItemById(It.IsAny<string>())).Returns<Comment>(x => It.IsAny<Comment>());

            var result = _sut.GetStructureOfComments(null);

            Assert.AreSame(null, result);
        }
    }
}
