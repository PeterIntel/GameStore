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
        private IMapper _mapper;

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
                Mapper.Initialize(cfg => cfg.CreateMap<TDomain, TEntity>());
                var item = Mapper.Map<TDomain, TEntity>(domainItem);
                _dbSet.Add(item);
            }
        }

        public IList<TDomain> GetAll(Expression<Func<TDomain, bool>> filterDomain, string includeProperties = "")
        {
            IQueryable<TEntity> queryToEntity = _dbSet.Where(x => x.IsDeleted == false);
            Mapper.Initialize(cfg => cfg.CreateMap<TDomain, TEntity>());
            var filterEntity = Mapper.Map<Expression<Func<TDomain, bool>>, Expression<Func<TEntity, bool>>>(filterDomain);

            if (filterEntity != null)
            {
                queryToEntity = queryToEntity.Where(filterEntity);
            }

            foreach (var includeProperty in includeProperties.Split(new char[]{','}, StringSplitOptions.RemoveEmptyEntries))
            {
                queryToEntity = queryToEntity.Include(includeProperty);
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
