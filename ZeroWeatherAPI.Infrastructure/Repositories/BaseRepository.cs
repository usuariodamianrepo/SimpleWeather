using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using ZeroWeatherAPI.Core.Interfaces.Repositories;
using ZeroWeatherAPI.Infrastructure.Data;

namespace ZeroWeatherAPI.Infrastructure.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        internal AppDbContext Context;
        internal DbSet<TEntity> dbSet;

        public BaseRepository(AppDbContext context)
        {
            this.Context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await dbSet.AddAsync(entity);
        }

        public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await dbSet.AddRangeAsync(entities);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, 
            string includeProperties = "",
            bool tracked = false,
            int take = 0
            )
        {
            IQueryable<TEntity> query = dbSet;

            if (tracked)
            {
                query = dbSet;
            }
            else
            {
                query = dbSet.AsNoTracking();
            }

            if (filter != null)
                query = query.Where(filter);

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if(take > 0)
                query = query.Take(take);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();

            return await query.ToListAsync();
        }

        public virtual async ValueTask<TEntity> GetByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual void Remove(TEntity entity)
        {
            dbSet.Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            dbSet.RemoveRange(entities);
        }

        public virtual async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await dbSet.SingleOrDefaultAsync(predicate);
        }

        public virtual async Task Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            Context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual async Task UpdateRange(IEnumerable<TEntity> entitiesToUpdate)
        {
            dbSet.AttachRange(entitiesToUpdate);
            Context.Entry(entitiesToUpdate).State = EntityState.Modified;
        }
    }
}
