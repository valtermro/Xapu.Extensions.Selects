using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Xapu.Extensions.Selects.Core
{
    internal class CollectionMapperExpressionBuilder : ITypeMapperExpressionBuilder
    {
        private static readonly MethodInfo SelectMethodInfo = typeof(Enumerable).GetMethods().First(p => p.Name == "Select");
        private static readonly MethodInfo ToListMethodInfo = typeof(Enumerable).GetMethods().First(p => p.Name == "ToList");
        private static readonly MethodInfo ToArrayMethodInfo = typeof(Enumerable).GetMethods().First(p => p.Name == "ToArray");

        private readonly IMapperExpressionBuilderContext _ctx;

        public CollectionMapperExpressionBuilder(IMapperExpressionBuilderContext ctx)
        {
            _ctx = ctx;
        }

        public Expression Build(Expression sourceLocalName, Type sourceType, Type resultType)
        {
            var sourceElementType = sourceType.GetCollectionElementType();
            var resultElementType = resultType.GetCollectionElementType();

            var selectMethod = SelectMethodInfo.MakeGenericMethod(sourceElementType, resultElementType);
            var selectorExpression = _ctx.CreateExpression(sourceElementType, resultElementType);
            var selectCallExpression = Expression.Call(selectMethod, sourceLocalName, selectorExpression);

            var newCollectionExpression = ResolveCollectionCasting(selectCallExpression, resultType, resultElementType);
            return newCollectionExpression;
        }

        private static Expression ResolveCollectionCasting(MethodCallExpression expression, Type resultType, Type resultElementType)
        {
            if (resultType == typeof(IEnumerable<>).MakeGenericType(resultElementType))
                return expression;

            if (resultType.IsArray)
                return Expression.Call(ToArrayMethodInfo.MakeGenericMethod(resultElementType), expression);

            return DefaultCollectionTypeExpression(expression, resultType, resultElementType);
        }

        private static Expression DefaultCollectionTypeExpression(MethodCallExpression expression, Type resultType, Type resultElementType)
        {
            var toListCallExpression = Expression.Call(ToListMethodInfo.MakeGenericMethod(resultElementType), expression);

            if (resultType == typeof(List<>).MakeGenericType(resultElementType))
                return toListCallExpression;

            // To give the final func the correct signature
            return Expression.TypeAs(toListCallExpression, resultType);
        }
    }
}
