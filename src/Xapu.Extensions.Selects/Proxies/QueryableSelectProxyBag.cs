using System;
using System.Collections.Concurrent;
using Xapu.Extensions.Selects.Core;

namespace Xapu.Extensions.Selects.Proxies
{
    internal static class QueryableSelectProxyBag
    {
        private static readonly ConcurrentDictionary<Type, IQueryableSelectProxy> Instances = new ConcurrentDictionary<Type, IQueryableSelectProxy>();

        public static IQueryableSelectProxy GetForType(Type type)
        {
            if (!Instances.ContainsKey(type))
                Instances[type] = CreateForType(type);

            return Instances[type];
        }

        private static IQueryableSelectProxy CreateForType(Type sourceType)
        {
            var proxyType = typeof(QueryableSelectProxy<>);
            var elementType = sourceType.GetCollectionElementType();

            var instanceType = proxyType.MakeGenericType(elementType);
            var instance = Activator.CreateInstance(instanceType);

            return (IQueryableSelectProxy)instance;
        }
    }
}
