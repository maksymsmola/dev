using System;
using System.Linq;
using System.Linq.Expressions;

namespace MoneyKeeper.Core
{
    public class Specification<T>
    {
        public virtual Expression<Func<T, bool>> Predicate { get; }

        protected Specification()
        {
        }

        public Specification(Expression<Func<T, bool>> predicate)
        {
            this.Predicate = predicate;
        }

        public static Specification<T> operator &(Specification<T> left, Specification<T> right)
        {
            BinaryExpression andAlsoExpression = Expression.AndAlso(left.Predicate, right.Predicate);

            Expression<Func<T, bool>> predicateExpression =
                Expression.Lambda<Func<T, bool>>(andAlsoExpression, left.Predicate.Parameters.Single());

            return new Specification<T>(predicateExpression);
        }

        public static Specification<T> operator |(Specification<T> left, Specification<T> right)
        {
            BinaryExpression orElseExpression = Expression.OrElse(left.Predicate, right.Predicate);

            Expression<Func<T, bool>> predicateExpression =
                Expression.Lambda<Func<T, bool>>(orElseExpression, left.Predicate.Parameters.Single());

            return new Specification<T>(predicateExpression);
        }
    }
}