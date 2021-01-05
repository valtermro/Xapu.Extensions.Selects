using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Xapu.Extensions.Selects
{
    internal class CollectionMapperExpressionBuilder
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
            var resultCollection = BuildResultCollectionExpression(sourceLocalName, sourceType, resultType);
            var castedCollection = ResolveCollectionCasting(resultCollection, resultType);
            
            return _ctx.ResolveNullGuard(sourceLocalName, castedCollection, sourceType, resultType);
        }

        private MethodCallExpression BuildResultCollectionExpression(Expression sourceLocalName, Type sourceType, Type resultType)
        {
            var sourceElementType = sourceType.GetCollectionElementType();
            var resultElementType = resultType.GetCollectionElementType();

            var methodInfo = SelectMethodInfo.MakeGenericMethod(sourceElementType, resultElementType);
            var selector = _ctx.CreateExpression(sourceElementType, resultElementType);
            
            return Expression.Call(methodInfo, sourceLocalName, selector);
        }

        private static Expression ResolveCollectionCasting(MethodCallExpression expression, Type resultType)
        {
            if (!NeedCasting(resultType))
                return expression;

            if (resultType.IsArray)
                return BuildArrayExpression(expression, resultType);
            else
                return BuildListExpression(expression, resultType);
        }

        private static bool NeedCasting(Type resultType)
        {
            var elementType = resultType.GetCollectionElementType();

            return resultType != typeof(IEnumerable<>).MakeGenericType(elementType);
        }

        private static Expression BuildArrayExpression(MethodCallExpression expression, Type resultType)
        {
            var elementType = resultType.GetCollectionElementType();
            var toArrayMethod = ToArrayMethodInfo.MakeGenericMethod(elementType);

            return Expression.Call(toArrayMethod, expression);
        }

        private static Expression BuildListExpression(MethodCallExpression expression, Type resultType)
        {
            var elementType = resultType.GetCollectionElementType();
            var toListMethod = ToListMethodInfo.MakeGenericMethod(elementType);

            return Expression.Call(toListMethod, expression);
        }
    }
}
