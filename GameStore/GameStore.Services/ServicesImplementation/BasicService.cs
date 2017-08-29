using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;
using GameStore.DataAccess.Interfaces;
using GameStore.DataAccess.Mongo.MongoEntities;
using GameStore.DataAccess.MSSQL.Entities;
using GameStore.DataAccess.UnitOfWork;
using GameStore.Domain.BusinessObjects;
using GameStore.Domain.ServicesInterfaces;
using GameStore.Logging.Loggers;

namespace GameStore.Services.ServicesImplementation
{
    public abstract class BasicService<TSqlEntity, TDomain> : ICrudService<TDomain> where TDomain : BasicDomain where TSqlEntity : BasicEntity
    {
        private readonly IGenericDataRepository<TSqlEntity, TDomain> _genericRepository;
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly IMongoLogger<TDomain> Logger;

        protected BasicService(IGenericDataRepository<TSqlEntity, TDomain> genericRepository, IUnitOfWork unitOfWork, IMongoLogger<TDomain> logger)
        {
            _genericRepository = genericRepository;
            UnitOfWork = unitOfWork;
            Logger = logger;
        }

        public virtual void Add(TDomain item)
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

        public virtual TDomain First(Expression<Func<TDomain, bool>> filter)
        {
            return _genericRepository.First(filter);
        }

        public virtual IEnumerable<TDomain> Get(params Expression<Func<TDomain, object>>[] includeProperties)
        {
            return _genericRepository.Get(includeProperties);
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

        public virtual void Update(TDomain item)
        {
            _genericRepository.Update(item);
            UnitOfWork.Save();
            var updatedItem = _genericRepository.GetItemById(item.Id);
            Logger.Write(Operation.Update, item, updatedItem);
        }

        public virtual IEnumerable<TDomain> Get(Expression<Func<TDomain, bool>> filter, params Expression<Func<TDomain, object>>[] includeProperties)
        {
            var result = _genericRepository.Get(filter, includeProperties);

            return result;
        }
    }
}
