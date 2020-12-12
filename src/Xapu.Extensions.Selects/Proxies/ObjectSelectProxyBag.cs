using System;
using System.Collections.Concurrent;

namespace Xapu.Extensions.Selects.Proxies
{
    internal static class ObjectSelectProxyBag
    {
        private static readonly ConcurrentDictionary<Type, IObjectSelectProxy> Instances = new ConcurrentDictionary<Type, IObjectSelectProxy>();

        public static IObjectSelectProxy GetForType(Type type)
        {
            if (!Instances.ContainsKey(type))
                Instances[type] = CreateForType(type);

            return Instances[type];
        }

        private static IObjectSelectProxy CreateForType(Type sourceType)
        {
            var proxyType = typeof(ObjectSelectProxy<>);

            var instanceType = proxyType.MakeGenericType(sourceType);
            var instance = Activator.CreateInstance(instanceType);

            return (IObjectSelectProxy)instance;
        }
    }
}
