// todo: encapsulate query logic using Specification pattern
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MoneyKeeper.Core.Entities;

namespace MoneyKeeper.DataAccess.Repository
{
    public interface IRepository
    {
        T FindById<T>(long id) where T : BaseEntity;

        T FindByCriteria<T>(Expression<Func<T, bool>> criteria) where T : BaseEntity;

        void Add<T>(T entity) where T : BaseEntity;

        void AddRange<T>(List<T> entities) where T : BaseEntity;

        List<T> GetByCriteria<T>(Expression<Func<T, bool>> criteria) where T : BaseEntity;

        List<T> GetByCriteriaIncluding<T>(Expression<Func<T, bool>> criteria, params Expression<Func<T, object>>[] include) where T : BaseEntity;

        void SaveChanges();
    }
}