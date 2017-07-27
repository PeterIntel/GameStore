using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using GameStore.Services.ServicesImplementation;
using GameStore.DataAccess.UnitOfWork;
using GameStore.Domain.BusinessObjects;
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
            _unitOfWork.Setup(g => g.CommentRepository.Remove(It.IsAny<int>()));

            _sut.Remove(133);

            _unitOfWork.Verify(u => u.CommentRepository.Remove(It.IsAny<int>()), Times.Once);
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
    }
}
