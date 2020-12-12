using System;
using System.Linq.Expressions;
using Xapu.Extensions.Selects.Core.Base;
using Xapu.Extensions.Selects.Core.ExpressionBuilders;
using Xapu.Extensions.Selects.Exceptions;

namespace Xapu.Extensions.Selects.Core.Mappers
{
    internal class MapperExpressionBuilder : IMapperExpressionBuilderContext
    {
        private readonly MapperExpressionConfig _config;
        private readonly IMapperExpressionBuilder _objectMapperExpressionBuilder;
        private readonly IMapperExpressionBuilder _nullableMapperExpressionBuilder;
        private readonly IMapperExpressionBuilder _basicTypeMapperExpressionBuilder;
        private readonly IMapperExpressionBuilder _collectionMapperExpressionBuilder;

        public MapperExpressionBuilder(MapperExpressionConfig config)
        {
            _config = config;
            _objectMapperExpressionBuilder = new ObjectMapperExpressionBuilder(this);
            _nullableMapperExpressionBuilder = new NullableMapperExpressionBuilder(this);
            _basicTypeMapperExpressionBuilder = new BasicTypeMapperExpressionBuilder();
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
