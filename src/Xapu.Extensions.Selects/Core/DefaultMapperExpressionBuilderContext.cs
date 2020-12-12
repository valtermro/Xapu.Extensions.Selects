using System;
using System.Linq.Expressions;
using Xapu.Extensions.Selects.Exceptions;

namespace Xapu.Extensions.Selects.Core
{
    internal class DefaultMapperExpressionBuilderContext : IMapperExpressionBuilderContext
    {
        private readonly MapperExpressionConfig _config;
        private readonly ITypeMapperExpressionBuilder _nullableMapperExpressionBuilder;
        private readonly ITypeMapperExpressionBuilder _basicTypeMapperExpressionBuilder;
        private readonly ITypeMapperExpressionBuilder _objectMapperExpressionBuilder;
        private readonly ITypeMapperExpressionBuilder _collectionMapperExpressionBuilder;

        public DefaultMapperExpressionBuilderContext(MapperExpressionConfig config)
        {
            _config = config;
            _nullableMapperExpressionBuilder = new NullableMapperExpressionBuilder(this);
            _basicTypeMapperExpressionBuilder = new BasicTypeMapperExpressionBuilder(this);
            _objectMapperExpressionBuilder = new ObjectMapperExpressionBuilder(this);
            _collectionMapperExpressionBuilder = new CollectionMapperExpressionBuilder(this);
        }

        public Expression CreateExpression(Type sourceType, Type resultType)
        {
            var param = Expression.Parameter(sourceType);
            var body = CreateExpression(param, sourceType, resultType);
            
            var lambda = Expression.Lambda(body, param);
            return lambda;
        }

        public Expression CreateExpression(Expression sourceLocalName, Type sourceType, Type resultType)
        {
            if (sourceType.IsNullable() || resultType.IsNullable())
                return _nullableMapperExpressionBuilder.Build(sourceLocalName, sourceType, resultType);

            if (sourceType.IsBasicType() && resultType.IsBasicType())
                return _basicTypeMapperExpressionBuilder.Build(sourceLocalName, sourceType, resultType);

            if (sourceType.IsObjectType() && resultType.IsObjectType())
                return _objectMapperExpressionBuilder.Build(sourceLocalName, sourceType, resultType);

            if (sourceType.IsCollectionType() && resultType.IsCollectionType())
                return _collectionMapperExpressionBuilder.Build(sourceLocalName, sourceType, resultType);

            throw new InvalidTypeMappingException(sourceType, resultType);
        }

        public Expression ResolveNullGuard(Expression sourceLocalName, Expression expression, Type sourceType, Type resultType)
        {
            if (!_config.GuardNull)
                return expression;

            var sourceIsNullExpression = Expression.Equal(sourceLocalName, Expression.Default(sourceType));
            var resultDefaultExpression = Expression.Default(resultType);
            return Expression.Condition(sourceIsNullExpression, resultDefaultExpression, expression);
        }
    }
}
