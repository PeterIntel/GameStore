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
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AccountService(IGenericDataRepository<UserEntity, User> userRepository, IRoleRepository roleRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<User> Get(params Expression<Func<User, object>>[] includeProperties)
        {
            return _userRepository.Get().ToList();
        }

        public void Add(User user)
        {
            AssignIdIfEmpty(user);
            user.Roles = user.IdRoles.Select(x => new Role() {RoleEnum = (RoleEnum) Enum.Parse(typeof(RoleEnum), x)});
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

        public IEnumerable<Role> GetAllRolesAndMarkSelected(IEnumerable<string> selecredRoles)
        {
            IEnumerable<Role> roles = _roleRepository.Get().ToList();
            if (selecredRoles != null)
            {
                foreach (var item in roles)
                {
                    if (selecredRoles.Contains(item.RoleEnum.ToString()))
                    {
                        item.IsChecked = true;
                    }
                }
            }

            return roles;
        }

        public User First(Expression<Func<User, bool>> filter)
        {
            return _userRepository.First(filter);
        }

        public IEnumerable<Role> GetRoles()
        {
            return _roleRepository.Get();
        }
    }
}
