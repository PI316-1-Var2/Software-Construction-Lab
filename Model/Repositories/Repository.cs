using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using Model.Interfaces;

namespace Model.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly CanteenContext Context;

        private IRepository<TEntity> This => this;

        public Repository(CanteenContext context)
        {
            Context = context;
        }

        TEntity IRepository<TEntity>.Get(int id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        IEnumerable<TEntity> IRepository<TEntity>.GetAll()
        {
            return Context.Set<TEntity>().ToList();
        }

        IEnumerable<TEntity> IRepository<TEntity>.Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate);
        }
        TEntity IRepository<TEntity>.SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().SingleOrDefault(predicate);
        }

        void IRepository<TEntity>.Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        void IRepository<TEntity>.AddRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().AddRange(entities);
        }

        void IRepository<TEntity>.Remove(int id)
        {
            var entry = Context.Entry(This.Get(id));
            entry.State = EntityState.Deleted;
        }

        void IRepository<TEntity>.RemoveRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
        }
    }
}
