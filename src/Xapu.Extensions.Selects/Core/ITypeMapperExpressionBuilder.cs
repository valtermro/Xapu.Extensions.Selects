using System;
using System.Linq.Expressions;

namespace Xapu.Extensions.Selects.Core
{
    internal interface ITypeMapperExpressionBuilder
    {
        Expression Build(Expression sourceLocalName, Type sourceType, Type resultType);
    }
}
