using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;

namespace Xapu.Extensions.Selects.Core.TypeBuilders
{
    internal static class TypeBuilderEnvironment
    {
        private static AssemblyBuilder DefaultAssemblyBuilder;
        private static ModuleBuilder DefaultModuleBuilder;

        public static string CreateUniqueTypeName(Type sourceType)
        {
            var name = CreateTypeName(sourceType);

            return TagUnique(name);
        }

        public static string CreateTypeKey(Type sourceType, IEnumerable<string> fields)
        {
            using var hash = MD5.Create();
            var baseKey = sourceType.FullName + string.Concat(fields);
            var baseKeyBytes = Encoding.UTF8.GetBytes(baseKey);

            return string.Concat(hash.ComputeHash(baseKeyBytes).Select(x => x.ToString("x2")));
        }

        private static string CreateTypeName(Type sourceType)
        {
            var name = sourceType.FullName.Replace(".", "_");
            return !sourceType.IsGenericType ? name : $"{name}_{sourceType.GenericTypeArguments.Length}";
        }

        private static string TagUnique(string name)
        {
            return $"{name}_{CreateNameToken()}";
        }

        public static TypeBuilder CreateTypeBuilder(string typeName)
        {
            var moduleBuilder = GetDefaultModuleBuilder();

            return moduleBuilder.DefineType(typeName, TypeAttributes.Public);
        }

        private static ModuleBuilder GetDefaultModuleBuilder()
        {
            return DefaultModuleBuilder ??= CreateDefaultModuleBuilder();
        }

        private static AssemblyBuilder GetDefaultAssemblyBuilder()
        {
            return DefaultAssemblyBuilder ??= CreateDefaultAssemblyBuilder();
        }

        private static ModuleBuilder CreateDefaultModuleBuilder()
        {
            var assemblyBuilder = GetDefaultAssemblyBuilder();
            var moduleNameToken = CreateNameToken();

            return assemblyBuilder.DefineDynamicModule(moduleNameToken);
        }

        private static AssemblyBuilder CreateDefaultAssemblyBuilder()
        {
            var assemblyNameToken = CreateNameToken();
            var assemblyName = new AssemblyName(assemblyNameToken);

            return AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
        }

        private static string CreateNameToken()
        {
            return Guid.NewGuid().ToString().Replace("-", string.Empty);
        }
    }
}
