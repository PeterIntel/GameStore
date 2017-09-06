using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using GameStore.DataAccess.Interfaces;
using GameStore.DataAccess.MSSQL.Entities;
using GameStore.Domain.BusinessObjects;

namespace GameStore.DataAccess.MSSQL.Repositories
{
    public class GenericDataRepository<TEntity, TDomain> : IGenericDataRepository<TEntity, TDomain> where TEntity : BasicEntity where TDomain : BasicDomain
    {
        protected GamesSqlContext _context;
        protected DbSet<TEntity> _dbSet;
        protected IMapper _mapper;

        public GenericDataRepository(GamesSqlContext context, IMapper mapper)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
            _mapper = mapper;
        }

        public virtual void Add(TDomain domainItem)
        {
            if (domainItem != null)
            {
                var item = _mapper.Map<TDomain, TEntity>(domainItem);
                item.IsSqlEntity = true;
                _dbSet.Add(item);
            }
        }

        public IEnumerable<TDomain> Get(Expression<Func<TDomain, bool>> filterDomain, params Expression<Func<TDomain, object>>[] includeProperties)
        {
            IQueryable<TEntity> queryToEntity = _dbSet.Where(x => x.IsDeleted == false);
            var filterEntity = _mapper.Map<Expression<Func<TDomain, bool>>, Expression<Func<TEntity, bool>>>(filterDomain);
            var includePropertiesForEntities = _mapper.Map<Expression<Func<TDomain, object>>[], Expression<Func<TEntity, object>>[]>(includeProperties);

            if (filterEntity != null)
            {
                queryToEntity = queryToEntity.Where(filterEntity);
            }

            foreach (var item in includePropertiesForEntities)
            {
                queryToEntity.Include(item);
            }

            var result = queryToEntity.ProjectTo<TDomain>(_mapper.ConfigurationProvider);

            return result;
        }

        public IEnumerable<TDomain> Get(params Expression<Func<TDomain, object>>[] includeProperties)
        {
            IQueryable<TEntity> queryToEntity = _dbSet.Where(x => x.IsDeleted == false);
            var includePropertiesForEntities = _mapper.Map<Expression<Func<TDomain, object>>[], Expression<Func<TEntity, object>>[]>(includeProperties);

            foreach (var item in includePropertiesForEntities)
            {
                queryToEntity.Include(item);
            }

            var result = queryToEntity.ProjectTo<TDomain>(_mapper.ConfigurationProvider);

            return result;
        }

        public virtual TDomain GetItemById(string id)
        {
            TEntity entity = _dbSet.Find(id);

            if (entity != null && entity.IsDeleted == false)
            {
                TDomain domain = _mapper.Map<TEntity, TDomain>(entity);

                return domain;
            }

            return null;
        }

        public virtual void Remove(TDomain item)
        {
            if (item != null)
            {
                TEntity entity = _dbSet.Find(item.Id);
                if (entity != null)
                {
                    entity.IsDeleted = true;
                    _context.Entry(entity).State = EntityState.Modified;
                }
            }
        }

        public virtual void Remove(string id)
        {
            TEntity entity = _dbSet.Find(id);

            if (entity != null)
            {
                entity.IsDeleted = true;
                _context.Entry(entity).State = EntityState.Modified;
            }
        }

        public virtual void Update(TDomain item)
        {
            if (item != null)
            {
                TEntity entity = _mapper.Map<TDomain, TEntity>(item);
                entity.IsSqlEntity = true;
                _context.Entry(_dbSet.Find(entity.Id)).State = EntityState.Detached;
                _context.Entry(entity).State = EntityState.Modified;
            }
        }

        public int GetCountObject(Expression<Func<TDomain, bool>> filter)
        {
            var filterEntity = _mapper.Map<Expression<Func<TDomain, bool>>, Expression<Func<TEntity, bool>>>(filter);
            IQueryable<TEntity> queryToEntity = _dbSet.Where(x => x.IsDeleted == false);

            if (filter != null)
            {
                queryToEntity = queryToEntity.Where(filterEntity);
            }

            var result = queryToEntity.Count();

            return result;
        }

        public virtual TDomain First(Expression<Func<TDomain, bool>> filter)
        {
            var filterEntity = _mapper.Map<Expression<Func<TDomain, bool>>, Expression<Func<TEntity, bool>>>(filter);
            if (filter != null)
            {
                IQueryable<TEntity> queryToEntities = _dbSet.Where(x => x.IsDeleted == false).Where(filterEntity);
                var d = queryToEntities.ToList();
                return queryToEntities.ProjectTo<TDomain>(_mapper.ConfigurationProvider).FirstOrDefault();
            }

            return null;
        }

        public bool Any(Expression<Func<TDomain, bool>> filter)
        {
            var s = _mapper.Map<User, UserEntity>(new User());
            var filterToEntity = _mapper.Map<Expression<Func<TDomain, bool>>, Expression<Func<TEntity, bool>>>(filter);
            var result = _dbSet.Any(filterToEntity);

            return result;
        }

        public IEnumerable<TDomain> LoadDomainEntities(IEnumerable<string> ids)
        {
            var domainEntities = new List<TDomain>();
            foreach (var id in ids)
            {
                var domainEntity = GetItemById(id);
                if (domainEntity != null) { domainEntities.Add(domainEntity); }
            }

            return domainEntities;
        }

        protected string GetGuidId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
