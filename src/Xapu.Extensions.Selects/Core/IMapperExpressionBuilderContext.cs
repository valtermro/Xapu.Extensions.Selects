using System;
using System.Linq.Expressions;

namespace Xapu.Extensions.Selects.Core
{
    internal interface IMapperExpressionBuilderContext
    {
        Expression CreateExpression(Type sourceType, Type resultType);
        Expression CreateExpression(Expression sourceLocalName, Type sourceType, Type resultType);
    }
}
