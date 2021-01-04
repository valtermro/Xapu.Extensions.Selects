using System;
using System.Collections.Generic;

namespace Xapu.Extensions.Selects
{
    internal class WithFieldsCollectionTypeBuilder
    {
        private readonly IWithFieldsTypeBuilderContext _ctx;

        public WithFieldsCollectionTypeBuilder(IWithFieldsTypeBuilderContext ctx)
        {
            _ctx = ctx;
        }

        public Type CreateType(Type collectionType, IEnumerable<string> newTypeFields)
        {
            var sourceElementType = collectionType.GetCollectionElementType();
            var resultElementType = _ctx.CreateType(sourceElementType, newTypeFields);

            return ResolveCollectionType(collectionType, resultElementType);
        }

        private static Type ResolveCollectionType(Type type, Type resultElementType)
        {
            if (type.IsArray)
                return resultElementType.MakeArrayType();

            if (type.IsGenericType)
                return type.GetGenericTypeDefinition().MakeGenericType(resultElementType);

            throw new InvalidCollectionTypeException(type);
        }
    }
}
