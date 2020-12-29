using System;
using System.Linq.Expressions;
using Xapu.Extensions.Selects.Core.Base;
using Xapu.Extensions.Selects.Exceptions;

namespace Xapu.Extensions.Selects.Core.ExpressionBuilders
{
    internal class NullableMapperExpressionBuilder : IMapperExpressionBuilder
    {
        private readonly IMapperExpressionBuilderContext _ctx;

        public NullableMapperExpressionBuilder(IMapperExpressionBuilderContext ctx)
        {
            _ctx = ctx;
        }

        public Expression Build(Expression sourceLocalName, Type sourceType, Type resultType)
        {
            if (resultType.IsNullable())
                return BuildToNullableExpression(sourceLocalName, sourceType, resultType);

            if (sourceType.IsNullable())
                return BuildFromNullableExpression(sourceLocalName, sourceType, resultType);

            throw new InvalidTypeMappingException(sourceType, resultType);
        }

        private Expression BuildToNullableExpression(Expression sourceLocalName, Type sourceType, Type resultType)
        {
            var resultElementType = resultType.GetNullableElementType();
            var resultValue = _ctx.CreateExpression(sourceLocalName, sourceType, resultElementType);

            return Expression.TypeAs(resultValue, resultType);
        }

        private Expression BuildFromNullableExpression(Expression sourceLocalName, Type sourceType, Type resultType)
        {
            var sourceElementType = sourceType.GetNullableElementType();

            var sourceHasValue = Expression.Property(sourceLocalName, sourceType.GetProperty("HasValue"));
            var sourceValue = Expression.Property(sourceLocalName, sourceType.GetProperty("Value"));
            var resultValue = _ctx.CreateExpression(sourceValue, sourceElementType, resultType);

            return Expression.Condition(sourceHasValue, resultValue, Expression.Default(resultType));
        }
    }
}
