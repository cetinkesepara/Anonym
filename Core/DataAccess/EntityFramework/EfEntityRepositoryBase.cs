using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
        private DbContext _context;
        public EfEntityRepositoryBase(DbContext context)
        {
            _context = context;
        }

        public TEntity Add(TEntity entity)
        {
            var addedEntity = _context.Entry(entity);
            addedEntity.State = EntityState.Added;
            _context.SaveChanges();

            return entity;
        }

        public void Delete(TEntity entity)
        {
            var deletedEntity = _context.Entry(entity);
            deletedEntity.State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public TEntity Get(System.Linq.Expressions.Expression<Func<TEntity, bool>> filter)
        {
            return _context.Set<TEntity>().SingleOrDefault(filter);
        }

        public IList<TEntity> GetList(System.Linq.Expressions.Expression<Func<TEntity, bool>> filter = null)
        {
            return filter == null
                ? _context.Set<TEntity>().ToList()
                : _context.Set<TEntity>().Where(filter).ToList();
        }

        public TEntity Update(TEntity entity)
        {
            var updatedEntity = _context.Entry(entity);
            updatedEntity.State = EntityState.Modified;
            _context.SaveChanges();

            return entity;
        }
    }
}
