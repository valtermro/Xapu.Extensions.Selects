using System;
using System.Collections;
using System.Linq;
using Xapu.Extensions.Selects.Exceptions;

namespace Xapu.Extensions.Selects.Core
{
    internal static class TypeExtensions
    {
        public static bool IsBasicType(this Type type)
        {
            return type.IsPrimitive ||
                type.IsEnum ||
                type == typeof(object) ||
                type == typeof(string) ||
                type == typeof(decimal) ||
                type == typeof(Guid) ||
                type == typeof(DateTime);
        }

        public static bool IsObjectType(this Type type)
        {
            return type.IsClass && !IsBasicType(type) && !IsCollectionType(type);
        }

        public static bool IsCollectionType(this Type type)
        {
            return typeof(IEnumerable).IsAssignableFrom(type) && type != typeof(string);
        }

        public static bool IsNullable(this Type type)
        {
            return type.IsGenericType &&
                   type.GetGenericTypeDefinition() == typeof(Nullable<>) &&
                   !type.IsGenericTypeDefinition;
        }

        public static Type GetCollectionElementType(this Type type)
        {
            if (type.IsArray)
                return type.GetElementType();

            if (type.IsGenericType)
                return type.GetGenericArguments().First();

            throw new InvalidCollectionTypeException(type);
        }
    }
}
