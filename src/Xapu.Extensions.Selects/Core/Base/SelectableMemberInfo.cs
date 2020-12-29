using System;
using System.Reflection;

namespace Xapu.Extensions.Selects.Core.Base
{
    internal enum MemberInfoKind
    {
        Property,
        Field
    }

    internal interface IMemberInfo
    {
        public MemberInfoKind Kind { get; }
        string Name { get; }
        Type Type { get; }
        MemberInfo OriginalInfo { get; }

    }

    internal class SelectableMemberInfo : IMemberInfo
    {
        public MemberInfoKind Kind { get; private set; }
        public string Name { get; private set; }
        public Type Type { get; private set; }
        public MemberInfo OriginalInfo { get; private set; }

        public static SelectableMemberInfo From(FieldInfo field)
        {
            return new SelectableMemberInfo
            {
                Kind = MemberInfoKind.Field,
                OriginalInfo = field,
                Name = field.Name,
                Type = field.FieldType
            };
        }

        public static SelectableMemberInfo From(PropertyInfo property)
        {
            return new SelectableMemberInfo
            {
                Kind = MemberInfoKind.Property,
                Name = property.Name,
                Type = property.PropertyType,
                OriginalInfo = property
            };
        }
    }
}
