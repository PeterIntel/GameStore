﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GameStore.DataAccess.Interfaces;
using GameStore.DataAccess.MSSQL.Entities;
using GameStore.DataAccess.UnitOfWork;
using GameStore.Domain.BusinessObjects;
using GameStore.Domain.ServicesInterfaces;
using GameStore.Logging.Loggers;
using GameStore.Services.Localization;

namespace GameStore.Services.ServicesImplementation
{
    public abstract class BasicService<TSqlEntity, TDomain> : ICrudService<TDomain> where TDomain : BasicDomain where TSqlEntity : BasicEntity
    {
        protected const string DefaultCultureCode = "en";

        private readonly IGenericDataRepository<TSqlEntity, TDomain> _genericRepository;
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly IMongoLogger<TDomain> Logger;
        protected readonly ILocalizationProvider<TDomain> LocalizationProvider;

        protected BasicService(IGenericDataRepository<TSqlEntity, TDomain> genericRepository, IUnitOfWork unitOfWork, IMongoLogger<TDomain> logger, ILocalizationProvider<TDomain> localizationProvider)
        {
            _genericRepository = genericRepository;
            UnitOfWork = unitOfWork;
            Logger = logger;
            LocalizationProvider = localizationProvider;
        }

        public virtual void Add(TDomain item, string cultureCode)
        {
            AssignIdIfEmpty(item);
            _genericRepository.Add(item);
            UnitOfWork.Save();
            Logger.Write(Operation.Insert, item);
        }

        public virtual bool Any(Expression<Func<TDomain, bool>> filter)
        {
            return _genericRepository.Any(filter);
        }

        protected void AssignIdIfEmpty(TDomain item)
        {
            if (item.Id == null)
            {
                item.Id = Guid.NewGuid().ToString();
            }
        }

        public virtual TDomain First(Expression<Func<TDomain, bool>> filter, string cultureCode)
        {
            var item = _genericRepository.First(filter);

            LocalizationProvider.Localize(item, cultureCode);
            return item;
        }

        public virtual IEnumerable<TDomain> Get(string cultureCode, params Expression<Func<TDomain, object>>[] includeProperties)
        {
            var items = _genericRepository.Get(includeProperties).ToList();

            foreach (var item in items)
            {
                LocalizationProvider.Localize(item, cultureCode);
            }

            return items;
        }

        public virtual void Remove(string id)
        {
            _genericRepository.Remove(id);
            UnitOfWork.Save();
        }

        public virtual void Remove(TDomain item)
        {
            _genericRepository.Remove(item);
            UnitOfWork.Save();
            Logger.Write(Operation.Delete, item);
        }

        public virtual void Update(TDomain item, string cultureCode)
        {
            _genericRepository.Update(item);
            UnitOfWork.Save();
            var updatedItem = _genericRepository.GetItemById(item.Id);
            Logger.Write(Operation.Update, item, updatedItem);
        }

        public virtual IEnumerable<TDomain> Get(Expression<Func<TDomain, bool>> filter, string cultureCode, params Expression<Func<TDomain, object>>[] includeProperties)
        {
            var items = _genericRepository.Get(filter, includeProperties).ToList();

            foreach (var item in items)
            {
                LocalizationProvider.Localize(item, cultureCode);
            }

            return items;
        }

        protected void CheckForNull(object objectToCheck, string exceptionMessage)
        {
            if (objectToCheck == null)
            {
                throw new ArgumentException(exceptionMessage);
            }
        }

        public TDomain GetItemById(string id)
        {
            var result = _genericRepository.GetItemById(id);

            return result;
        }
    }
}
