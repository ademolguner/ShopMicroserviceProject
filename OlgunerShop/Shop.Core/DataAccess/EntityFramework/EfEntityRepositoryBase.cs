using Microsoft.EntityFrameworkCore;
using Shop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Shop.Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        #region Async Process

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            using (var context = new TContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                await context.SaveChangesAsync();
                return entity;
            }
        }

        public async Task DeleteAsync(TEntity entity)
        {
            using (var context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                await context.SaveChangesAsync();
            }
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter)
        {
            var context = new TContext();
            return filter == null ?
                await context.Set<TEntity>().FirstOrDefaultAsync() :
                await context.Set<TEntity>().Where(filter).FirstOrDefaultAsync();
        }

        public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            var context = new TContext();
            return filter == null ?
                await context.Set<TEntity>().ToListAsync() :
                await context.Set<TEntity>().Where(filter).ToListAsync();
        }

        public IQueryable<TEntity> GetListQuery(Expression<Func<TEntity, bool>> filter = null)
        {
            var context = new TContext();
            return filter == null ?
                 context.Set<TEntity>().AsQueryable() :
                 context.Set<TEntity>().Where(filter).AsQueryable();
        }

        public IQueryable<TEntity> GetListQuery(Expression<Func<TEntity, bool>> filter, int page, int pagesize)
        {
            var context = new TContext();
            var skip = (page - 1) * pagesize;
            return filter == null ?
                context.Set<TEntity>().AsQueryable() :
                context.Set<TEntity>().Where(filter).Skip(skip).Take(pagesize).AsQueryable();
        }

        public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter, int page, int pagesize)
        {
            var context = new TContext();
            var skip = (page - 1) * pagesize;
            return filter == null ?
                await context.Set<TEntity>().Skip(skip).Take(pagesize).ToListAsync() :
                await context.Set<TEntity>().Where(filter).Skip(skip).Take(pagesize).ToListAsync();
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            using (var context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                await context.SaveChangesAsync();
                return entity;
            }
        }

        #endregion Async Process
    }
}