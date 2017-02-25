using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using MoneyKeeper.Core.Entities;

namespace MoneyKeeper.DataAccess.Repository
{
    public class Repository : IRepository
    {
        private readonly DbContext context;

        public Repository(DbContext context)
        {
            this.context = context;
        }

        public T FindById<T>(long id) where T : BaseEntity
        {
            return this.context.Set<T>().Find(id);
        }

        public T FindByCriteria<T>(Expression<Func<T, bool>> criteria) where T : BaseEntity
        {
            return this.context.Set<T>().FirstOrDefault(criteria);
        }

        public void Add<T>(T entity) where T : BaseEntity
        {
            this.context.Set<T>().Add(entity);
        }

        public void AddRange<T>(List<T> entities) where T : BaseEntity
        {
            this.context.Set<T>().AddRange(entities);
        }

        public List<T> GetByCriteria<T>(Expression<Func<T, bool>> criteria) where T : BaseEntity
        {
            return this.context.Set<T>().Where(criteria).ToList();
        }

        public List<T> GetByCriteriaIncluding<T>(Expression<Func<T, bool>> criteria, params Expression<Func<T, object>>[] include) where T : BaseEntity
        {
            IQueryable<T> query = this.context.Set<T>();
            foreach (Expression<Func<T, object>> expression in include)
            {
                query = query.Include(expression);
            }

            return query.Where(criteria).ToList();
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }
    }
}