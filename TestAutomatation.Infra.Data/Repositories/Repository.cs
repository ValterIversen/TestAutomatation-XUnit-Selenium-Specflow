using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TestAutomatation.Domain.Interface;
using TestAutomatation.Infra.Data.Context;

namespace TestAutomatation.Infra.Data.Repositories
{
    public abstract class Repository : IRepository
    {
        private readonly Context.AppContext _context;
        private bool disposedValue;

        protected Context.AppContext Context { get => _context; }

        public Repository(Context.AppContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }
                disposedValue = true;
            }
        }

        void IDisposable.Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }

    public abstract class Repository<TEntity> : Repository, IRepository<TEntity>
        where TEntity : class
    {
        public Repository(Context.AppContext context)
            : base(context)
        {
        }

        public virtual IEnumerable<TEntity> ObterTodos()
        {
            return DbSet.ToList();
        }

        private DbSet<TEntity> _dbSet;

        private DbSet<TEntity> CreateDbSet()
        {
            return Context.Set<TEntity>();
        }

        protected DbSet<TEntity> DbSet
        {
            get { return _dbSet ??= CreateDbSet(); }
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return DbSet.AsEnumerable();
        }

        public virtual void Insert(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            DbSet.Add(entity);
        }

        public virtual void Insert(IEnumerable<TEntity> entities)
        {
            if (!entities.Any() || entities is null)
                throw new ArgumentNullException(nameof(entities));

            DbSet.AddRange(entities);
        }

        public virtual void Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            DbSet.Update(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            DbSet.Remove(entity);
        }

        public virtual async Task<TEntity> OneAsync(Expression<Func<TEntity, bool>> expression)
            => await DbSet.FirstOrDefaultAsync(expression);

        public async Task<TEntity> OneAsync(Expression<Func<TEntity, bool>> expression, params string[] includes)
        {
            var q = DbSet.AsQueryable();

            if (includes.Any())
                foreach (var include in includes)
                    q = q.Include(include);

            return await q.FirstOrDefaultAsync(expression);
        }

        public virtual async Task<TEntity> OneAsyncNoTracking(Expression<Func<TEntity, bool>> expression)
            => await DbSet.AsNoTracking().FirstOrDefaultAsync(expression);

        public virtual IEnumerable<TEntity> Filter(Expression<Func<TEntity, bool>> exp, params string[] includes)
        {
            var q = DbSet.Where(exp);

            if (includes.Length > 0)
                foreach (var include in includes)
                    q = q.Include(include);

            return q.ToList();
        }

        public virtual TEntity Find(object key)
        {
            return DbSet.Find(key);
        }

        public Task<TEntity> FindAsync(object key)
        {
            return DbSet.FindAsync(key).AsTask();
        }
    }

}
