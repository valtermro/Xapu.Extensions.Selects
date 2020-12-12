using System;
using System.Linq.Expressions;
using Xapu.Extensions.Selects.Exceptions;

namespace Xapu.Extensions.Selects.Core
{
    internal class BasicTypeMapperExpressionBuilder : ITypeMapperExpressionBuilder
    {
        private readonly IMapperExpressionBuilderContext _ctx;

        public BasicTypeMapperExpressionBuilder(IMapperExpressionBuilderContext ctx)
        {
            _ctx = ctx;
        }

        public Expression Build(Expression sourceLocalName, Type sourceType, Type resultType)
        {
            if (sourceType == resultType)
                return sourceLocalName;

            throw new InvalidTypeMappingException(sourceType, resultType);
        }
    }
}
