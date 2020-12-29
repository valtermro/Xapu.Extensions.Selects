using System;
using System.Collections.Generic;
using Xapu.Extensions.Selects.Core.Base;

namespace Xapu.Extensions.Selects.Core.TypeBuilders
{
    internal interface IWithFieldsTypeBuilderContext
    {
        IMemberInfo GetTypeMember(Type sourceType, string fieldName);
        IEnumerable<string> GetTypeMemberNames(Type type);
        Type CreateType(Type sourceType, IEnumerable<string> newTypeFields);
    }
}
