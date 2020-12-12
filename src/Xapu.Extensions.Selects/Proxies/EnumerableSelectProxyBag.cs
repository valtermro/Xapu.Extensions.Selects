using System;
using System.Collections.Concurrent;
using Xapu.Extensions.Selects.Core;

namespace Xapu.Extensions.Selects.Proxies
{
    internal static class EnumerableSelectProxyBag
    {
        private static readonly ConcurrentDictionary<Type, IEnumerableSelectProxy> Instances = new ConcurrentDictionary<Type, IEnumerableSelectProxy>();
        
        public static IEnumerableSelectProxy GetForType(Type type)
        {
            if (!Instances.ContainsKey(type))
                Instances[type] = CreateForType(type);

            return Instances[type];
        }

        private static IEnumerableSelectProxy CreateForType(Type sourceType)
        {
            var proxyType = typeof(EnumerableSelectProxy<>);
            var elementType = sourceType.GetCollectionElementType();

            var instanceType = proxyType.MakeGenericType(elementType);
            var instance =  Activator.CreateInstance(instanceType);

            return (IEnumerableSelectProxy)instance;
        }
    }
}
