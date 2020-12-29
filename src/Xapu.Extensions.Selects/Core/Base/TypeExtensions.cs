using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xapu.Extensions.Selects.Exceptions;

namespace Xapu.Extensions.Selects.Core.Base
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

        public static IMemberInfo GetReadableMember(this Type type, string name)
        {
            return GetReadableMembers(type).FirstOrDefault(p => p.Name == name);
        }

        public static IMemberInfo GetWritableMember(this Type type, string name)
        {
            return GetWritableMembers(type).FirstOrDefault(p => p.Name == name);
        }

        public static IEnumerable<IMemberInfo> GetReadableMembers(this Type type)
        {
            foreach (var field in GetReadableFields(type))
                yield return SelectableMemberInfo.From(field);

            foreach (var property in GetReadableProperties(type))
                yield return SelectableMemberInfo.From(property);
        }

        public static IEnumerable<IMemberInfo> GetWritableMembers(this Type type)
        {
            foreach (var field in GetWritableFields(type))
                yield return SelectableMemberInfo.From(field);

            foreach (var property in GetWritableProperties(type))
                yield return SelectableMemberInfo.From(property);
        }

        private static IEnumerable<FieldInfo> GetReadableFields(Type type)
        {
            return type.GetFields();
        }

        private static IEnumerable<FieldInfo> GetWritableFields(Type type)
        {
            return type.GetFields();
        }

        private static IEnumerable<PropertyInfo> GetReadableProperties(Type type)
        {
            return type.GetProperties().Where(p => p.GetMethod != null && p.GetMethod.IsPublic);
        }

        private static IEnumerable<PropertyInfo> GetWritableProperties(Type type)
        {
            return type.GetProperties().Where(p => p.SetMethod != null && p.SetMethod.IsPublic);
        }

        public static Type GetNullableElementType(this Type type)
        {
            return type.GetGenericArguments().First();
        }

        public static Type GetCollectionElementType(this Type type)
        {
            if (type.GenericTypeArguments.Length == 1)
                return type.GenericTypeArguments[0];

            var iface = type.GetInterface("IEnumerable`1");
            
            if (iface != null)
                return iface.GenericTypeArguments[0];

            throw new InvalidCollectionTypeException(type);
        }
    }
}
