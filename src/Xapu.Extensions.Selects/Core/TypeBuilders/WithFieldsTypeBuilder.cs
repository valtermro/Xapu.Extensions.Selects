using System;
using System.Collections.Generic;
using System.Linq;

namespace Xapu.Extensions.Selects
{
    internal class WithFieldsTypeBuilder : IWithFieldsTypeBuilderContext
    {
        private readonly WithFieldsObjectTypeBuilder _objectTypeBuilder;
        private readonly WithFieldsCollectionTypeBuilder _collectionTypeBuilder;

        public WithFieldsTypeBuilder()
        {
            _objectTypeBuilder = new WithFieldsObjectTypeBuilder(this);
            _collectionTypeBuilder = new WithFieldsCollectionTypeBuilder(this);
        }

        public Type CreateType(Type sourceType, IEnumerable<string> newTypeFields)
        {
            if (sourceType.IsBasicType() || sourceType.IsNullable())
                return sourceType;

            if (sourceType.IsObjectType())
                return _objectTypeBuilder.CreateType(sourceType, newTypeFields);

            if (sourceType.IsCollectionType())
                return _collectionTypeBuilder.CreateType(sourceType, newTypeFields);

            throw new Exception($"Cannot create WithFieldsType based on {sourceType}");
        }

        public ITypeMemberInfo GetTypeMember(Type sourceType, string fieldName)
        {
            var member = sourceType.GetReadableMember(fieldName);

            if (member == null)
                throw new UnreachableMemberException(fieldName);

            return member;
        }

        public IEnumerable<string> GetTypeMemberNames(Type type)
        {
            if (type.IsObjectType())
                return type.GetReadableMembers().Select(p => p.Name);

            if (type.IsCollectionType())
                return GetSourceCollectionElementMemberNames(type);

            return null;
        }

        private IEnumerable<string> GetSourceCollectionElementMemberNames(Type type)
        {
            var elementType = type.GetCollectionElementType();

            if (!elementType.IsObjectType() && !elementType.IsBasicType() && !elementType.IsNullable())
                throw new InvalidCollectionTypeException(type);

            return GetTypeMemberNames(elementType);
        }
    }
}
