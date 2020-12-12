using System;
using System.Linq.Expressions;
using Xapu.Extensions.Selects.Exceptions;

namespace Xapu.Extensions.Selects.Core
{
    internal class DefaultMapperExpressionBuilderContext : IMapperExpressionBuilderContext
    {
        private readonly ITypeMapperExpressionBuilder _nullableMapperExpressionBuilder;
        private readonly ITypeMapperExpressionBuilder _basicTypeMapperExpressionBuilder;
        private readonly ITypeMapperExpressionBuilder _objectMapperExpressionBuilder;
        private readonly ITypeMapperExpressionBuilder _collectionMapperExpressionBuilder;

        public DefaultMapperExpressionBuilderContext()
        {
            _nullableMapperExpressionBuilder = new NullableMapperExpressionBuilder(this);
            _basicTypeMapperExpressionBuilder = new BasicTypeMapperExpressionBuilder(this);
            _objectMapperExpressionBuilder = new ObjectMapperExpressionBuilder(this);
            _collectionMapperExpressionBuilder = new CollectionMapperExpressionBuilder(this);
        }

        public LambdaExpression CreateMapperExpression(Type sourceType, Type resultType)
        {
            var param = Expression.Parameter(sourceType);
            var body = ResolveMapperExpression(param, sourceType, resultType);

            return Expression.Lambda(body, param);
        }

        private Expression ResolveMapperExpression(Expression param, Type sourceType, Type resultType)
        {
            if (sourceType.IsNullable() || resultType.IsNullable())
                return _nullableMapperExpressionBuilder.Build(param, sourceType, resultType);

            if (sourceType.IsBasicType() && resultType.IsBasicType())
                return _basicTypeMapperExpressionBuilder.Build(param, sourceType, resultType);

            if (sourceType.IsObjectType() && resultType.IsObjectType())
                return _objectMapperExpressionBuilder.Build(param, sourceType, resultType);

            if (sourceType.IsCollectionType() && resultType.IsCollectionType())
                return _collectionMapperExpressionBuilder.Build(param, sourceType, resultType);

            throw new InvalidTypeMappingException(sourceType, resultType);
        }
    }
}
