using System;
using System.Linq.Expressions;

namespace Xapu.Extensions.Selects.Core
{
    internal static class MapperExpressionBuilder
    {
        public static Expression<Func<TSource, TResult>> Build<TSource, TResult>()
        {
            var ctx = new DefaultMapperExpressionBuilderContext();
            var lambda = ctx.CreateExpression(typeof(TSource), typeof(TResult));

            return (Expression<Func<TSource, TResult>>)lambda;
        }
    }
}
