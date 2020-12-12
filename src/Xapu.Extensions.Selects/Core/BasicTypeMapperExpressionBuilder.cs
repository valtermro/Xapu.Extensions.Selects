using System;
using System.Linq.Expressions;

namespace Xapu.Extensions.Selects.Core
{
    internal class BasicTypeMapperExpressionBuilder : ITypeMapperExpressionBuilder
    {
        private IMapperExpressionBuilderContext _ctx;

        public BasicTypeMapperExpressionBuilder(IMapperExpressionBuilderContext ctx)
        {
            _ctx = ctx;
        }

        public Expression Build(Expression sourceLocalName, Type sourceType, Type resultType)
        {
            throw new NotImplementedException();
        }
    }
}
