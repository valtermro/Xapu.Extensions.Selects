using System;
using System.Collections.Generic;
using System.Linq;
using Xapu.Extensions.Selects.Tests.Base;
using Xapu.Extensions.Selects.Tests.FixtureTypes;
using Xunit;

namespace Xapu.Extensions.Selects.Tests.SelectFieldsTests
{
    public class ExtensionOverloadings
    {
        [Fact]
        public void WithVarArgs()
        {
            var create = Creator.New(() => new ObjectType
            {
                Id = 1,
                Value = "01",
                Parent = new ObjectType()
            });

            static void Assertions(dynamic result)
            {
                Assert.Equal(1, result.Id);
                Assert.Equal("01", result.Value);
            }

            Assertions(create.Array().SelectFields("Id", "Value").First());
            Assertions(create.Array().SelectFields("Id", "Value").First());
            Assertions(create.Queryable().SelectFields("Id", "Value").First());
            Assertions(create.Queryable().SelectFields("Id", "Value").First());
        }

        [Fact]
        public void WithEnumerableParameter()
        {
            var create = Creator.New(() => new ObjectType
            {
                Id = 1,
                Value = "01",
                Parent = new ObjectType()
            });

            var fieldList = new List<string> { "Id", "Value" };
            var fieldArray = new[] { "Id", "Value" };

            static void Assertions(dynamic result, Type resultType)
            {
                Assert.Equal(result.GetType().GetGenericTypeDefinition(), resultType.GetGenericTypeDefinition());

                var first = ((IEnumerable<dynamic>)result).First();

                Assert.Equal(1, first.Id);
                Assert.Equal("01", first.Value);
            }

            var enumerableT = EnumerableUtils.GetArrayIteratorType<ObjectType>();
            var queryableT = typeof(EnumerableQuery<ObjectType>);

            Assertions(create.Array().SelectFields(fieldArray), enumerableT);
            Assertions(create.Array().SelectFields(fieldList), enumerableT);

            Assertions(create.Queryable().SelectFields(fieldArray), queryableT);
            Assertions(create.Queryable().SelectFields(fieldList), queryableT);
        }
    }
}
