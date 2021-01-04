using System;
using System.Linq.Expressions;

namespace Xapu.Extensions.Selects
{
    internal interface IMapperExpressionBuilderContext
    {
        Expression CreateExpression(Type sourceType, Type resultType);
        Expression CreateExpression(Expression sourceLocalName, Type sourceType, Type resultType);
        Expression ResolveNullGuard(Expression sourceLocalName, Expression expression, Type sourceType, Type resultType);
    }
}
