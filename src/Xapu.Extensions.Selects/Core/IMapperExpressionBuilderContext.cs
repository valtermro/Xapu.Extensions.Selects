using System;
using System.Linq.Expressions;

namespace Xapu.Extensions.Selects.Core
{
    internal interface IMapperExpressionBuilderContext
    {
        LambdaExpression CreateMapperExpression(Type sourceType, Type resultType);
    }
}
