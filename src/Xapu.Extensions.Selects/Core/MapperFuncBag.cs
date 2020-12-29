using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using Xapu.Extensions.Selects.Core.ExpressionBuilders;

namespace Xapu.Extensions.Selects.Core
{
    internal static class MapperFuncBag
    {
        private interface IKey<S, R> { }
        private static readonly ConcurrentDictionary<Type, object> Instances = new ConcurrentDictionary<Type, object>();

        public static Func<TSource, TResult> Get<TSource, TResult>()
        {
            var key = typeof(IKey<TSource, TResult>);

            if (!Instances.ContainsKey(key))
                Instances[key] = Create<TSource, TResult>();

            return (Func<TSource, TResult>)Instances[key];
        }

        private static Func<TSource, TResult> Create<TSource, TResult>()
        {
            var config = new MapperExpressionConfig(guardNull: true);
            var builder = new MapperExpressionBuilder(config);
            var expression = builder.CreateExpression(typeof(TSource), typeof(TResult));

            var lambda = (Expression<Func<TSource, TResult>>)expression;
            return lambda.Compile();
        }
    }
}
