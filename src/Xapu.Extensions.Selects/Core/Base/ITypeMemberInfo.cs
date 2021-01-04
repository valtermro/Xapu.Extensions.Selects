using System;
using System.Reflection;

namespace Xapu.Extensions.Selects
{
    internal interface ITypeMemberInfo
    {
        public TypeMemberInfoKind Kind { get; }
        string Name { get; }
        Type Type { get; }
        MemberInfo OriginalInfo { get; }

    }
}
