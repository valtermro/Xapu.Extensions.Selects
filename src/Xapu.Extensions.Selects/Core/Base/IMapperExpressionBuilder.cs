using System;
using System.Linq.Expressions;

namespace Xapu.Extensions.Selects.Core.Base
{
    internal interface IMapperExpressionBuilder
    {
        Expression Build(Expression sourceLocalName, Type sourceType, Type resultType);
    }
}
