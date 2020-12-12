using System;
using System.Linq.Expressions;
using Xapu.Extensions.Selects.Core.Base;
using Xapu.Extensions.Selects.Exceptions;

namespace Xapu.Extensions.Selects.Core.ExpressionBuilders
{
    internal class BasicTypeMapperExpressionBuilder : IMapperExpressionBuilder
    {
        public Expression Build(Expression sourceLocalName, Type sourceType, Type resultType)
        {
            if (sourceType == resultType)
                return sourceLocalName;

            throw new InvalidTypeMappingException(sourceType, resultType);
        }
    }
}
