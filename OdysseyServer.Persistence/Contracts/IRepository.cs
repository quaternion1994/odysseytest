using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OdysseyServer.Persistence.Contracts
{
    public interface IRepository<TEntity>
    {
        Task<List<TEntity>> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        Task<TEntity> GetByID(object id);
        Task Insert(TEntity entity);
        Task Delete(object id);
        Task Update(TEntity entityToUpdate);
        Task InsertMany(IEnumerable<TEntity> entities);
        Task SaveChangesAsync();
    }
}
