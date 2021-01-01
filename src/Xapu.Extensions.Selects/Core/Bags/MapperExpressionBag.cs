using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace Xapu.Extensions.Selects
{
    internal static class MapperExpressionBag
    {
        private interface IKey<S, R> { }
        private static readonly ConcurrentDictionary<Type, object> Instances = new ConcurrentDictionary<Type, object>();

        public static Expression<Func<TSource, TResult>> Get<TSource, TResult>()
        {
            var key = typeof(IKey<TSource, TResult>);

            if (!Instances.ContainsKey(key))
                Instances[key] = Create(typeof(TSource), typeof(TResult));

            return (Expression<Func<TSource, TResult>>)Instances[key];
        }

        public static LambdaExpression Get(Type sourceType, Type resultType)
        {
            var key = typeof(IKey<,>).MakeGenericType(sourceType, resultType);

            if (!Instances.ContainsKey(key))
                Instances[key] = Create(sourceType, resultType);

            return (LambdaExpression)Instances[key];
        }

        private static Expression Create(Type sourceType, Type resultType)
        {
            var config = new MapperExpressionConfig(guardNull: false);
            var builder = new MapperExpressionBuilder(config);

            return builder.CreateExpression(sourceType, resultType);
        }
    }
}
