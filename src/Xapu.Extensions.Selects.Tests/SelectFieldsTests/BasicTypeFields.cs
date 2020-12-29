using System;
using System.Linq;
using Xapu.Extensions.Selects.Tests.Base;
using Xapu.Extensions.Selects.Tests.FixtureTypes;
using Xunit;

namespace Xapu.Extensions.Selects.Tests.SelectFieldsTests
{
    public class BasicTypeFields
    {
        private static readonly Guid Guid = Guid.NewGuid();
        private static readonly DateTime DateTime = DateTime.Now;

        [Fact]
        public void BasicSelection()
        {
            var create = Creator.New(() => new BasicTypes
            {
                Guid = Guid,
                DateTime = DateTime,
                Enum = StubEnum.Two,
                Bool = true,
                Char = 'C',
                String = "S",
                Byte = 1,
                Decimal = 2
            });

            var fields = new[] { "Guid", "DateTime", "Enum", "Bool", "Char", "String", "Byte" };

            static void Assertions(dynamic result)
            {
                Assert.Equal(Guid, result.Guid);
                Assert.Equal(DateTime, result.DateTime);
                Assert.Equal(StubEnum.Two, result.Enum);
                Assert.Equal(true, result.Bool);
                Assert.Equal('C', result.Char);
                Assert.Equal("S", result.String);
                Assert.Equal(1, AssertX.Type(result.Byte, typeof(byte)));

                Assert.Null(result.GetType().GetField("Decimal"));
            };

            Assertions(create.Array().SelectFields(fields).First());
            Assertions(create.Queryable().SelectFields(fields).First());
        }
    }
}
