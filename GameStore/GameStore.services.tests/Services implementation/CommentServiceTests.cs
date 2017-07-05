using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using GameStore.Services.Services_implementation;
using GameStore.DataAccess.UnitOfWork;
using GameStore.Domain.Business_objects;
using System.Linq.Expressions;

namespace GameStore.Services.Tests.Services_implementation
{
    [TestFixture]
    class CommentServiceTests
    {
        private CommentService _sut;
        private Mock<IUnitOfWork> _unitOfWork;
        private Comment _commentStub = new Comment();

        [SetUp]
        public void Setup()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _sut = new CommentService(_unitOfWork.Object);
        }

        [Test]
        public void Add_IsCalled_CalledOneTime()
        {
            _unitOfWork.Setup(p => p.Comments.Add(It.IsAny<Comment>()));

            _sut.Add(new Comment());

            _unitOfWork.Verify(u => u.Comments.Add(It.IsAny<Comment>()), Times.Once);
        }

        [Test]
        public void Add_ExceptoinThrown_DoesnotCatch()
        {
            _unitOfWork.Setup(g => g.Comments.Add(It.IsAny<Comment>()))
                .Throws(new Exception());

            Assert.Throws<Exception>(() => _sut.Add(It.IsAny<Comment>()));
        }

        [Test]
        public void GetAllCommentsByGameKey_IsCalled_CalledOneTime()
        {
            _unitOfWork.Setup(g => g.Comments.GetAll(It.IsAny<Expression<Func<Comment, bool>>>(), It.IsAny<string>()))
                .Returns(() => It.IsAny<IList<Comment>>());

            _sut.GetAllCommentsByGameKey("game");

            _unitOfWork.Verify(u => u.Comments.GetAll(It.IsAny<Expression<Func<Comment, bool>>>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void GetAllCommentsByGameKey_ExceptoinThrown_DoesnotCatch()
        {
            _unitOfWork.Setup(g => g.Comments.GetAll(It.IsAny<Expression<Func<Comment, bool>>>(), It.IsAny<string>()))
               .Throws(new Exception());

            Assert.Throws<Exception>(() => _sut.GetAllCommentsByGameKey(It.IsAny<string>()));
        }

        [Test]
        public void RemoteCommentId_IsCalled_CalledOneTime()
        {
            _unitOfWork.Setup(g => g.Comments.Remove(It.IsAny<int>()));

            _sut.Remove(133);

            _unitOfWork.Verify(u => u.Comments.Remove(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void RemoteCommentId_ExceptoinThrown_DoesnotCatch()
        {
            _unitOfWork.Setup(g => g.Comments.Remove(It.IsAny<int>()))
               .Throws(new Exception());

            Assert.Throws<Exception>(() => _sut.Remove(It.IsAny<int>()));
        }

        [Test]
        public void RemoteComment_IsCalled_CalledOneTime()
        {
            _unitOfWork.Setup(g => g.Comments.Remove(It.IsAny<Comment>()));

            _sut.Remove(_commentStub);

            _unitOfWork.Verify(u => u.Comments.Remove(It.IsAny<Comment>()), Times.Once);
        }

        [Test]
        public void RemoteComment_ExceptoinThrown_DoesnotCatch()
        {
            _unitOfWork.Setup(g => g.Comments.Remove(It.IsAny<Comment>()))
               .Throws(new Exception());

            Assert.Throws<Exception>(() => _sut.Remove(It.IsAny<Comment>()));
        }

        [Test]
        public void UpdateComment_IsCalled_CalledOneTime()
        {
            _unitOfWork.Setup(g => g.Comments.Update(It.IsAny<Comment>()));

            _sut.Update(_commentStub);

            _unitOfWork.Verify(u => u.Comments.Update(It.IsAny<Comment>()), Times.Once);
        }

        [Test]
        public void UpdateComment_ExceptoinThrown_DoesnotCatch()
        {
            _unitOfWork.Setup(g => g.Comments.Update(It.IsAny<Comment>()))
               .Throws(new Exception());

            Assert.Throws<Exception>(() => _sut.Update(It.IsAny<Comment>()));
        }
    }
}
