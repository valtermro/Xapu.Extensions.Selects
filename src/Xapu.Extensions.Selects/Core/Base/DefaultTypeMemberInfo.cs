using System;
using System.Reflection;

namespace Xapu.Extensions.Selects
{
    internal class DefaultTypeMemberInfo : ITypeMemberInfo
    {
        public TypeMemberInfoKind Kind { get; private set; }
        public string Name { get; private set; }
        public Type Type { get; private set; }
        public MemberInfo OriginalInfo { get; private set; }

        public static DefaultTypeMemberInfo From(FieldInfo field)
        {
            return new DefaultTypeMemberInfo
            {
                Kind = TypeMemberInfoKind.Field,
                OriginalInfo = field,
                Name = field.Name,
                Type = field.FieldType
            };
        }

        public static DefaultTypeMemberInfo From(PropertyInfo property)
        {
            return new DefaultTypeMemberInfo
            {
                Kind = TypeMemberInfoKind.Property,
                Name = property.Name,
                Type = property.PropertyType,
                OriginalInfo = property
            };
        }
    }
}
