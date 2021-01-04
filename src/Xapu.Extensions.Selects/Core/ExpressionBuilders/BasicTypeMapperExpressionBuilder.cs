using System;
using System.Linq.Expressions;

namespace Xapu.Extensions.Selects
{
    internal class BasicTypeMapperExpressionBuilder
    {
        public Expression Build(Expression sourceLocalName, Type sourceType, Type resultType)
        {
            if (sourceType == resultType)
                return sourceLocalName;

            throw new InvalidTypeMappingException(sourceType, resultType);
        }
    }
}
