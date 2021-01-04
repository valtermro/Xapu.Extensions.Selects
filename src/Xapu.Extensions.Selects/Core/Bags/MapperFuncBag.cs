using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace Xapu.Extensions.Selects
{
    internal static class MapperFuncBag
    {
        private interface IKey<S, R> { }
        private static readonly ConcurrentDictionary<Type, object> Instances = new ConcurrentDictionary<Type, object>();

        public static Func<TSource, TResult> Get<TSource, TResult>()
        {
            var key = typeof(IKey<TSource, TResult>);

            if (!Instances.ContainsKey(key))
                Instances[key] = Create(typeof(TSource), typeof(TResult));

            return (Func<TSource, TResult>)Instances[key];
        }

        public static Delegate Get(Type sourceType, Type resultType)
        {
            var key = typeof(IKey<,>).MakeGenericType(sourceType, resultType);

            if (!Instances.ContainsKey(key))
                Instances[key] = Create(sourceType, resultType);
            
            return (Delegate)Instances[key];
        }

        private static Delegate Create(Type sourceType, Type resultType)
        {
            var config = new MapperExpressionConfig(guardNull: true);
            var builder = new MapperExpressionBuilder(config);
            var expression = builder.CreateExpression(sourceType, resultType);

            var lambda = (LambdaExpression)expression;
            return lambda.Compile();
        }
    }
}
