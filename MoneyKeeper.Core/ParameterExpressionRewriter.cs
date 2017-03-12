using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MoneyKeeper.Core
{
    public class ParameterExpressionRewriter : ExpressionVisitor
    {
        private readonly Dictionary<ParameterExpression, ParameterExpression> parameterExpressionMap;

        public ParameterExpressionRewriter(IEnumerable<ParameterExpression> firstParams, IEnumerable<ParameterExpression> secondParams )
        {
            this.parameterExpressionMap =
                firstParams
                    .Zip(secondParams, (firstParam, secondParam) => new { firstParam, secondParam })
                    .ToDictionary(key => key.secondParam, value => value.firstParam);
        }

        protected override Expression VisitParameter(ParameterExpression parameterExpression)
        {
            ParameterExpression replacement;

            if (this.parameterExpressionMap.TryGetValue(parameterExpression, out replacement))
            {
                parameterExpression = replacement;
            }

            return base.VisitParameter(parameterExpression);
        }
    }
}