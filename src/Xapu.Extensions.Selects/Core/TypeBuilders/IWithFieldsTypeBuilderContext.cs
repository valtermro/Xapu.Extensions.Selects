using System;
using System.Collections.Generic;

namespace Xapu.Extensions.Selects
{
    internal interface IWithFieldsTypeBuilderContext
    {
        ITypeMemberInfo GetTypeMember(Type sourceType, string fieldName);
        IEnumerable<string> GetTypeMemberNames(Type type);
        Type CreateType(Type sourceType, IEnumerable<string> newTypeFields);
    }
}
