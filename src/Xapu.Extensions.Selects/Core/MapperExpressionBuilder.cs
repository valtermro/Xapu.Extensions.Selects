using System;
using System.Linq.Expressions;

namespace Xapu.Extensions.Selects.Core
{
    internal static class MapperExpressionBuilder
    {
        public static Expression<Func<TSource, TResult>> Build<TSource, TResult>(MapperExpressionConfig config)
        {
            var ctx = new DefaultMapperExpressionBuilderContext(config);
            var lambda = ctx.CreateExpression(typeof(TSource), typeof(TResult));

            return (Expression<Func<TSource, TResult>>)lambda;
        }
    }
}
