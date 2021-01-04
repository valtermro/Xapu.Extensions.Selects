using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Xapu.Extensions.Selects
{
    internal static class WithFieldsTypeBag
    {
        private static readonly ConcurrentDictionary<string, Type> Types = new ConcurrentDictionary<string, Type>();

        public static Type Get(Type sourceType, IEnumerable<string> fields)
        {
            var key = TypeBuilderEnvironment.CreateTypeKey(sourceType, fields);

            if (!Types.ContainsKey(key))
                Types[key] = Create(sourceType, fields);

            return Types[key];
        }

        private static Type Create(Type sourceType, IEnumerable<string> fields)
        {
            var builder = new WithFieldsTypeBuilder();
            
            return builder.CreateType(sourceType, fields);
        }
    }
}
