using System;
using System.Linq.Expressions;
using Xapu.Extensions.Selects.Exceptions;

namespace Xapu.Extensions.Selects.Core
{
    internal class NullableMapperExpressionBuilder : ITypeMapperExpressionBuilder
    {
        private readonly IMapperExpressionBuilderContext _ctx;

        public NullableMapperExpressionBuilder(IMapperExpressionBuilderContext ctx)
        {
            _ctx = ctx;
        }

        public Expression Build(Expression sourceLocalName, Type sourceType, Type resultType)
        {
            if (resultType.IsNullable())
            {
                var resultElementType = resultType.GetNullableElementType();
                var mappedValueExpression = _ctx.CreateExpression(sourceLocalName, sourceType, resultElementType);

                var resultExpression = Expression.TypeAs(mappedValueExpression, resultType);
                return resultExpression;
            }

            if (sourceType.IsNullable())
            {
                var sourceElementType = sourceType.GetNullableElementType();
                
                var sourceHasValueExpression = Expression.Property(sourceLocalName, sourceType.GetProperty("HasValue"));
                var sourceValueExpression = Expression.Property(sourceLocalName, sourceType.GetProperty("Value"));

                var resultValueExpression = _ctx.CreateExpression(sourceValueExpression, sourceElementType, resultType);
                var resultDefaultExpression = Expression.Default(resultType);

                var resultExpression = Expression.Condition(sourceHasValueExpression, resultValueExpression, resultDefaultExpression);
                return resultExpression;
            }

            throw new InvalidTypeMappingException(sourceType, resultType);
        }
    }
}
