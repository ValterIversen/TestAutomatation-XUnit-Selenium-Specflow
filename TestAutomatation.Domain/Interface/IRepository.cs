using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TestAutomatation.Domain.Interface
{
    public interface IRepository : IDisposable
    {
    }

    public interface IRepository<TEntity> : IRepository
        where TEntity : class
    {
        IEnumerable<TEntity> Filter(Expression<Func<TEntity, bool>> exp, params string[] includes);
        void Insert(TEntity entity);
        void Insert(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        IEnumerable<TEntity> GetAll();
        Task<TEntity> OneAsync(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> OneAsyncNoTracking(Expression<Func<TEntity, bool>> expression);
    }
}
