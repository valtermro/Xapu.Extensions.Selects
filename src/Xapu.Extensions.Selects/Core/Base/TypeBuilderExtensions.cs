using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Xapu.Extensions.Selects.Core.Base
{
    internal static class TypeBuilderExtensions
    {
        public static FieldBuilder DefinePublicField(this TypeBuilder builder, string fieldName, Type fieldType)
        {
            return builder.DefineField(fieldName, fieldType, FieldAttributes.Public);
        }
    }
}
