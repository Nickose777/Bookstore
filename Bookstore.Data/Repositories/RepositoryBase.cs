using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Bookstore.Data.Contracts.Repositories;

namespace Bookstore.Data.Repositories
{
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly BookstoreDbContext context;
        private readonly DbSet<TEntity> dbSet;

        public RepositoryBase(BookstoreDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public void Remove(TEntity entity)
        {
            dbSet.Remove(entity);
        }

        public TEntity Get(int id)
        {
            return dbSet.Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return dbSet.ToList();
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> expression)
        {
            return dbSet.Where(expression).ToList();
        }
    }
}
