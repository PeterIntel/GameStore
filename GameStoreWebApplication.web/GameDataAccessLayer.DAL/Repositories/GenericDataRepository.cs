using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.contracts.DomainModels;
using GameDataAccessLayer.DAL;

namespace GameDataAccessLayer.DAL.Repositories
{
    class GenericDataRepository<TEntity> : IGenericDataRepository<TEntity> where TEntity : BasicEntity
    {
        protected GamesContext _context;
        protected DbSet<TEntity> _dbSet;

        public GenericDataRepository(GamesContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public void Add(TEntity item)
        {
            if (item != null)
            {
                _dbSet.Add(item);
            }
        }

        public IList<TEntity> GetAll(Expression<Func<TEntity, bool>> filter, string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(new char[]{','}, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return query.Where(x => x.IsDeleted == false).ToList();
        }

        public TEntity GetItemById(int id)
        {
            TEntity entity = _dbSet.Find(id);

            if (entity != null && entity.IsDeleted == false)
            {
                return entity;
            }

            return null;
        }

        public void Remove(TEntity item)
        {
            if (item != null)
            {
                if (_context.Entry(item).State == EntityState.Detached)
                {
                    _dbSet.Attach(item);
                }

                item.IsDeleted = true;
            }
        }

        public void Remove(int id)
        {
            TEntity entity = _dbSet.Find(id);

            if (entity != null)
            {
                Remove(entity);
            }
        }

        public void Update(TEntity item)
        {
            if (item != null)
            {
                _dbSet.Attach(item);
                _context.Entry(item).State = EntityState.Modified;
            }
        }
    }
}
