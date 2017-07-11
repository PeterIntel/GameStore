using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GameStore.DataAccess.Entities;
using GameStore.Domain.BusinessObjects;
using AutoMapper;
using GameStore.DataAccess.Context;

namespace GameStore.DataAccess.Repositories
{
    public class GenericDataRepository<TEntity, TDomain> : IGenericDataRepository<TEntity, TDomain> where TEntity : BasicEntity where TDomain : class
    {
        protected GamesContext _context;
        protected DbSet<TEntity> _dbSet;
        protected IMapper _mapper;

        public GenericDataRepository(GamesContext context, IMapper mapper)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
            _mapper = mapper;
        }

        public void Add(TDomain domainItem)
        {
            if (domainItem != null)
            {
                var item = _mapper.Map<TDomain, TEntity>(domainItem);
                _dbSet.Add(item);
            }
        }

        public IEnumerable<TDomain> GetAll(Expression<Func<TDomain, bool>> filterDomain,params Expression<Func<TDomain, object>>[] includeProperties)
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

            var resultOfQuery = queryToEntity.ToList();
            var result = _mapper.Map<IList<TEntity>, IList<TDomain>>(resultOfQuery);

            return result;
        }

        public IEnumerable<TDomain> GetAll(params Expression<Func<TDomain, object>>[] includeProperties)
        {
            IQueryable<TEntity> queryToEntity = _dbSet.Where(x => x.IsDeleted == false);

            var includePropertiesForEntities = _mapper.Map<Expression<Func<TDomain, object>>[], Expression<Func<TEntity, object>>[]>(includeProperties);

            foreach (var item in includePropertiesForEntities)
            {
                queryToEntity.Include(item);
            }

            var resultOfQuery = queryToEntity.ToList();
            var result = _mapper.Map<IList<TEntity>, IList<TDomain>>(resultOfQuery);

            return result;
        }

        public TDomain GetItemById(int id)
        {
            TEntity entity = _dbSet.Find(id);

            if (entity != null && entity.IsDeleted == false)
            {
                TDomain domain = _mapper.Map<TEntity, TDomain>(entity);
                return domain;
            }

            return null;
        }

        public void Remove(TDomain item)
        {
            if (item != null)
            {
                TEntity entity = _mapper.Map<TDomain, TEntity>(item);
                entity.IsDeleted = true;
            }
        }

        public void Remove(int id)
        {
            TEntity entity = _dbSet.Find(id);

            if (entity != null)
            {
                TDomain domain = Mapper.Map<TEntity, TDomain>(entity);
                Remove(domain);
            }
        }

        public void Update(TDomain item)
        {
            if (item != null)
            {
                TEntity entity = Mapper.Map<TDomain, TEntity>(item);
                _dbSet.Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
            }
        }
    }
}
