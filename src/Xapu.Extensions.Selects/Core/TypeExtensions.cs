﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        public static IEnumerable<PropertyInfo> GetReadableProperties(this Type type)
        {
            return type.GetProperties().Where(p => p.GetMethod != null && p.GetMethod.IsPublic);
        }

        public static IEnumerable<PropertyInfo> GetWritableProperties(this Type type)
        {
            return type.GetProperties().Where(p => p.SetMethod != null && p.SetMethod.IsPublic);
        }

        public static Type GetNullableElementType(this Type type)
        {
            return type.GetGenericArguments().First();
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