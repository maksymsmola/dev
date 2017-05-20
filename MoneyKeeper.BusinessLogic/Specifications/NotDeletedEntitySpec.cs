using System;
using System.Linq.Expressions;
using MoneyKeeper.Core;
using MoneyKeeper.Core.Entities;

namespace MoneyKeeper.BusinessLogic.Specifications
{
    public class NotDeletedEntitySpec<T> : Specification<T> where T : StatefulEntity
    {
        public override Expression<Func<T, bool>> Predicate
        {
            get
            {
                return x => x.State != EntityState.Deleted;
            }
        }
    }
}