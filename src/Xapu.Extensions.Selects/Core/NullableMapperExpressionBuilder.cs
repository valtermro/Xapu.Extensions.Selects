using System;
using System.Linq.Expressions;

namespace Xapu.Extensions.Selects.Core
{
    internal class NullableMapperExpressionBuilder : ITypeMapperExpressionBuilder
    {
        private IMapperExpressionBuilderContext _ctx;

        public NullableMapperExpressionBuilder(IMapperExpressionBuilderContext ctx)
        {
            _ctx = ctx;
        }

        public Expression Build(Expression sourceLocalName, Type sourceType, Type resultType)
        {
            throw new NotImplementedException();
        }
    }
}
