using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;

namespace Xapu.Extensions.Selects
{
    internal class WithFieldsObjectTypeBuilder
    {
        private readonly IWithFieldsTypeBuilderContext _ctx;

        public WithFieldsObjectTypeBuilder(IWithFieldsTypeBuilderContext ctx)
        {
            _ctx = ctx;
        }

        public Type CreateType(Type sourceType, IEnumerable<string> newTypeFields)
        {
            var typeBuilder = CreateTypeBuilder(sourceType);

            AddFields(typeBuilder, sourceType, newTypeFields);

            return typeBuilder.CreateType();
        }

        private static TypeBuilder CreateTypeBuilder(Type sourceType)
        {
            var typeName = TypeBuilderEnvironment.CreateUniqueTypeName(sourceType);

            return TypeBuilderEnvironment.CreateTypeBuilder(typeName);
        }

        private void AddFields(TypeBuilder typeBuilder, Type sourceType, IEnumerable<string> fields)
        {
            var fieldGroups = fields.GroupBy(p => p.Substring(0, p.IndexOf('.') + 1));

            var shallowFieldNames = fieldGroups.Where(p => string.IsNullOrEmpty(p.Key));
            var nestedFieldNames = fieldGroups.Where(p => !string.IsNullOrEmpty(p.Key));

            AddShallowFields(typeBuilder, sourceType, shallowFieldNames);
            AddNestedFields(typeBuilder, sourceType, nestedFieldNames);
        }

        private void AddShallowFields(TypeBuilder typeBuilder, Type sourceType, IEnumerable<IGrouping<string, string>> fieldGroups)
        {
            foreach (var fieldName in fieldGroups.SelectMany(p => p))
            {
                var sourceMember = _ctx.GetTypeMember(sourceType, fieldName);
                var newFieldNames = _ctx.GetTypeMemberNames(sourceMember.Type);

                CreateField(typeBuilder, sourceMember, newFieldNames);
            }
        }

        private void AddNestedFields(TypeBuilder typeBuilder, Type sourceType, IEnumerable<IGrouping<string, string>> fieldGroups)
        {
            foreach (var group in fieldGroups)
            {
                var sourceMember = _ctx.GetTypeMember(sourceType, group.Key[0..^1]);
                var newFieldNames = group.Select(p => p[group.Key.Length..]);

                CreateField(typeBuilder, sourceMember, newFieldNames);
            }
        }

        private void CreateField(TypeBuilder typeBuilder, ITypeMemberInfo sourceMember, IEnumerable<string> newTypeFields)
        {
            var fieldType = _ctx.CreateType(sourceMember.Type, newTypeFields);

            typeBuilder.DefinePublicField(sourceMember.Name, fieldType);
        }
    }
}