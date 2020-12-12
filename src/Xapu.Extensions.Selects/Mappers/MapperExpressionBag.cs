﻿using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using Xapu.Extensions.Selects.Core;

namespace Xapu.Extensions.Selects.Mappers
{
    internal static class MapperExpressionBag
    {
        private interface IKey<S, R> { }
        private static readonly ConcurrentDictionary<Type, object> Instances = new ConcurrentDictionary<Type, object>();

        public static Expression<Func<TSource, TResult>> Get<TSource, TResult>()
        {
            var key = typeof(IKey<TSource, TResult>);

            if (!Instances.ContainsKey(key))
                Instances[key] = Create<TSource, TResult>();

            return (Expression<Func<TSource, TResult>>)Instances[key];
        }

        private static object Create<TSource, TResult>()
        {
            var expression = MapperExpressionBuilder.Build<TSource, TResult>();
            return expression;
        }
    }
}
