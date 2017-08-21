using GameStore.Domain.ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Domain.BusinessObjects;
using GameStore.DataAccess.Interfaces;
using GameStore.DataAccess.MSSQL.Entities;
using GameStore.DataAccess.UnitOfWork;
using System.Linq.Expressions;

namespace GameStore.Services.ServicesImplementation
{
    public class AccountService : BasicService<User>, IAccountService
    {
        private readonly IGenericDataRepository<UserEntity, User> _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public AccountService(IGenericDataRepository<UserEntity, User> userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<User> Get(params Expression<Func<User, object>>[] includeProperties)
        {
            return _userRepository.Get().ToList();
        }

        public void Add(User user)
        {
            AssignIdIfEmpty(user);
            _userRepository.Add(user);
            _unitOfWork.Save();
        }

        public void Update(User user)
        {
            _userRepository.Update(user);
            _unitOfWork.Save();
        }

        public void Remove(User user)
        {
            _userRepository.Remove(user);
            _unitOfWork.Save();
        }

        public void Remove(string id)
        {
            _userRepository.Remove(id);
            _unitOfWork.Save();
        }

        public bool Any(Expression<Func<User, bool>> filter)
        {
            return _userRepository.Any(filter);
        }
    }
}
