using System;
using System.Linq.Expressions;

namespace Xapu.Extensions.Selects
{
    internal class MapperExpressionBuilder : IMapperExpressionBuilderContext
    {
        private readonly MapperExpressionConfig _config;
        private readonly BasicTypeMapperExpressionBuilder _basicTypeMapperExpressionBuilder;
        private readonly NullableMapperExpressionBuilder _nullableMapperExpressionBuilder;
        private readonly ObjectMapperExpressionBuilder _objectMapperExpressionBuilder;
        private readonly CollectionMapperExpressionBuilder _collectionMapperExpressionBuilder;

        public MapperExpressionBuilder(MapperExpressionConfig config)
        {
            _config = config;
            _basicTypeMapperExpressionBuilder = new BasicTypeMapperExpressionBuilder();
            _nullableMapperExpressionBuilder = new NullableMapperExpressionBuilder(this);
            _objectMapperExpressionBuilder = new ObjectMapperExpressionBuilder(this);
            _collectionMapperExpressionBuilder = new CollectionMapperExpressionBuilder(this);
        }

        public Expression CreateExpression(Type sourceType, Type resultType)
        {
            var param = Expression.Parameter(sourceType);
            var body = CreateExpression(param, sourceType, resultType);
            
            return Expression.Lambda(body, param);
        }

        public Expression CreateExpression(Expression sourceLocalName, Type sourceType, Type resultType)
        {
            if (sourceType.IsObjectType() && resultType.IsObjectType())
                return _objectMapperExpressionBuilder.Build(sourceLocalName, sourceType, resultType);

            if (sourceType.IsNullable() || resultType.IsNullable())
                return _nullableMapperExpressionBuilder.Build(sourceLocalName, sourceType, resultType);

            if (sourceType.IsBasicType() && resultType.IsBasicType())
                return _basicTypeMapperExpressionBuilder.Build(sourceLocalName, sourceType, resultType);

            if (sourceType.IsCollectionType() && resultType.IsCollectionType())
                return _collectionMapperExpressionBuilder.Build(sourceLocalName, sourceType, resultType);

            throw new InvalidTypeMappingException(sourceType, resultType);
        }

        public Expression ResolveNullGuard(Expression sourceLocalName, Expression resultValue, Type sourceType, Type resultType)
        {
            if (!_config.GuardNull)
                return resultValue;

            var sourceIsNull = Expression.Equal(sourceLocalName, Expression.Default(sourceType));
            var defaultResultValue = Expression.Default(resultType);
            return Expression.Condition(sourceIsNull, defaultResultValue, resultValue);
        }
    }
}
