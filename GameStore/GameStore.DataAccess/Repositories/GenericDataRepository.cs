using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GameStore.DataAccess.Entities;
using GameStore.Domain.Business_objects;
using AutoMapper;

namespace GameStore.DataAccess.Repositories
{
    public class GenericDataRepository<TEntity, TDomain> : IGenericDataRepository<TEntity, TDomain> where TEntity : BasicEntity where TDomain : BasicDomainEntity
    {
        protected GamesContext _context;
        protected DbSet<TEntity> _dbSet;

        public GenericDataRepository(GamesContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public void Add(TDomain domainItem)
        {
            if (domainItem != null)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<TDomain, TEntity>());
                var item = Mapper.Map<TDomain, TEntity>(domainItem);
                _dbSet.Add(item);
            }
        }

        public IList<TDomain> GetAll(Expression<Func<TDomain, bool>> filter, string includeProperties = "")
        {
            IQueryable<TEntity> queryToEntity = _dbSet;
            Mapper.Initialize(cfg => cfg.CreateMap<TEntity, TDomain>());
            var queryToDomain = Mapper.Map<IQueryable<TEntity>, IQueryable<TDomain>>(queryToEntity);

            if (filter != null)
            {
                queryToDomain = queryToDomain.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(new char[]{','}, StringSplitOptions.RemoveEmptyEntries))
            {
                queryToDomain = queryToDomain.Include(includeProperty);
            }

            return queryToDomain.Where(x => x.IsDeleted == false).ToList();
        }

        public TDomain GetItemById(int id)
        {
            TEntity entity = _dbSet.Find(id);

            if (entity != null && entity.IsDeleted == false)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<TEntity, TDomain>());
                TDomain domain = Mapper.Map<TEntity, TDomain>(entity);
                return domain;
            }

            return null;
        }

        public void Remove(TDomain item)
        {
            if (item != null)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<TDomain, TEntity>());
                TEntity entity = Mapper.Map<TDomain, TEntity>(item);
                entity.IsDeleted = true;
            }
        }

        public void Remove(int id)
        {
            TEntity entity = _dbSet.Find(id);

            if (entity != null)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<TEntity, TDomain>());
                TDomain domain = Mapper.Map<TEntity, TDomain>(entity);
                Remove(domain);
            }
        }

        public void Update(TDomain item)
        {
            if (item != null)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<TDomain, TEntity>());
                TEntity entity = Mapper.Map<TDomain, TEntity>(item);
                _dbSet.Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
            }
        }
    }
}
