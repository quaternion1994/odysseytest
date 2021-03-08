using Microsoft.EntityFrameworkCore;
using OdysseyServer.Persistence.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OdysseyServer.Persistence.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class 
    {
        internal OdysseyDbContext context;
        internal DbSet<TEntity> dbSet;

        public Repository(OdysseyDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public async virtual Task<List<TEntity>> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public async virtual Task<TEntity> GetByID(object id)
        {
            return await dbSet.FindAsync(id);
        }

        public async virtual Task Insert(TEntity entity)
        {
            dbSet.Add(entity);
            await context.SaveChangesAsync();
        }

        public async virtual Task InsertMany(IEnumerable<TEntity> entities)
        {
            dbSet.AddRange(entities);
            await context.SaveChangesAsync();
        }

        public async virtual Task Delete(object id)
        {
            TEntity entityToDelete = await dbSet.FindAsync(id);
            await Delete(entityToDelete);
        }

        public async virtual Task Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
            await context.SaveChangesAsync();
        }

        public async virtual Task Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async virtual Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
