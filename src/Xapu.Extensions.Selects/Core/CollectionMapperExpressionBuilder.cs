using System;
using System.Linq.Expressions;

namespace Xapu.Extensions.Selects.Core
{
    internal class CollectionMapperExpressionBuilder : ITypeMapperExpressionBuilder
    {
        private readonly IMapperExpressionBuilderContext _ctx;

        public CollectionMapperExpressionBuilder(IMapperExpressionBuilderContext ctx)
        {
            _ctx = ctx;
        }

        public Expression Build(Expression sourceLocalName, Type sourceType, Type resultType)
        {
            throw new NotImplementedException();
        }
    }
}
