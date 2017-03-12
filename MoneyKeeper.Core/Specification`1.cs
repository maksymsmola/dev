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
            Expression<Func<T, bool>> leftPredicate = left.Predicate;
            Expression<Func<T, bool>> rightPredicate = right.Predicate;

            BinaryExpression andAlsoExpression =
                Expression.AndAlso(
                    leftPredicate.Body,
                    new ParameterExpressionRewriter(leftPredicate.Parameters, rightPredicate.Parameters).Visit(rightPredicate.Body));

            Expression<Func<T, bool>> predicateExpression =
                Expression.Lambda<Func<T, bool>>(andAlsoExpression, leftPredicate.Parameters);

            return new Specification<T>(predicateExpression);
        }

        public static Specification<T> operator |(Specification<T> left, Specification<T> right)
        {
            Expression<Func<T, bool>> leftPredicate = left.Predicate;
            Expression<Func<T, bool>> rightPredicate = right.Predicate;

            BinaryExpression orElseExpression =
                Expression.OrElse(
                    leftPredicate.Body,
                    new ParameterExpressionRewriter(leftPredicate.Parameters, rightPredicate.Parameters).Visit(rightPredicate.Body));

            Expression<Func<T, bool>> predicateExpression =
                Expression.Lambda<Func<T, bool>>(orElseExpression, left.Predicate.Parameters.Single());

            return new Specification<T>(predicateExpression);
        }
    }
}