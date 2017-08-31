﻿using GameStore.Domain.ServicesInterfaces;
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
using GameStore.Logging.Loggers;

namespace GameStore.Services.ServicesImplementation
{
    public class AccountService : BasicService<UserEntity, User>, IAccountService
    {
        private readonly IGenericDataRepository<UserEntity, User> _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IGenericDataRepository<PublisherEntity, Publisher> _publisherRepository;

        public AccountService(IGenericDataRepository<UserEntity, User> userRepository, IRoleRepository roleRepository, IUnitOfWork unitOfWork, IMongoLogger<User> logger, IGenericDataRepository<PublisherEntity, Publisher> publisherRepository) : base(userRepository, unitOfWork, logger)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _publisherRepository = publisherRepository;
        }

        public override void Add(User user)
        {
            AssignIdIfEmpty(user);
            if (user.IdRoles != null)
            {
                user.Roles = user.IdRoles.Select(x => new Role() {RoleEnum = (RoleEnum) Enum.Parse(typeof(RoleEnum), x)});
            }

            if (user.Publisher != null)
            {
                user.Publisher = _publisherRepository.GetItemById(user.Publisher.Id);
            }

            _userRepository.Add(user);
            UnitOfWork.Save();
        }

        public override void Update(User user)
        {
            user.Roles = user.IdRoles.Select(x => new Role() { RoleEnum = (RoleEnum)Enum.Parse(typeof(RoleEnum), x) });
            if (user.Publisher != null)
            {
                user.Publisher = _publisherRepository.GetItemById(user.Publisher.Id);
            }
            _userRepository.Update(user);
            UnitOfWork.Save();
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

        public int GetCountAdministrators()
        {
            var result = _userRepository.GetCountObject(x => x.Roles.Select(role => role.RoleEnum).Contains(RoleEnum.Administrator));

            return result;
        }

        public IEnumerable<Role> GetRoles()
        {
            var roles = _roleRepository.Get();

            return roles;
        }
    }
}
