using System;
using System.Collections.Concurrent;
using Xapu.Extensions.Selects.Core;

namespace Xapu.Extensions.Selects.Mappers
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
            var expression = MapperExpressionBuilder.Build<TSource, TResult>();
            return expression.Compile();
        }
    }
}
