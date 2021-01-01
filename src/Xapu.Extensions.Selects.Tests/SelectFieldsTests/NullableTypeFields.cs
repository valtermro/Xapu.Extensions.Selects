using System.Linq;
using Xunit;

namespace Xapu.Extensions.Selects.Tests.SelectFieldsTests
{
    public class NullableTypeFields
    {
        [Fact]
        public void NullableFields()
        {
            var create = Creator.New(() => new NullableValues
            {
                NullableInt = 1
            });

            var fields = new[] { "NullableInt" };

            static void Assertions(dynamic result)
            {
                var field = result.GetType().GetField("NullableInt");

                Assert.Equal(typeof(int?), field.FieldType);
                Assert.Equal(1, result.NullableInt);
            };

            Assertions(create.Array().SelectFields(fields).First());
            Assertions(create.Queryable().SelectFields(fields).First());
        }
    }
}
