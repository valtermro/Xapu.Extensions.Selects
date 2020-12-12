using System;
using System.Linq.Expressions;

namespace Xapu.Extensions.Selects.Core
{
    internal static class MapperExpressionBuilder
    {
        public static Expression<Func<TSource, TResult>> Build<TSource, TResult>()
        {
            var builderCtx = new DefaultMapperExpressionBuilderContext();
            var lambda = builderCtx.CreateMapperExpression(typeof(TSource), typeof(TResult));

            return (Expression<Func<TSource, TResult>>)lambda;
        }
    }
}
