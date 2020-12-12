using System;
using System.Linq;
using Xapu.Extensions.Selects.Tests.FixtureTypes;
using Xunit;

namespace Xapu.Extensions.Selects.Tests
{
    public class BasicTypeMapping
    {
        [Fact]
        public void SameTypeMapping()
        {
            var source = new BasicTypes
            {
                Guid = Guid.NewGuid(),
                DateTime = DateTime.Now,
                Enum = StubEnum.Two,
                Bool = true,
                Char = 'C',
                String = "S",
                Byte = 1,
                Decimal = 2,
                Double = 3,
                Float = 4,
                Int = 5,
                Long = 6,
                Object = 7,
                Sbyte = 8,
                Short = 9,
                Uint = 10,
                Ulong = 11,
                Ushort = 12
            };

            RunAssertions(source.ToNew<BasicTypesView>());
            RunAssertions(new[] { source }.SelectNew<BasicTypesView>().First());
            RunAssertions(new[] { source }.AsQueryable().SelectNew<BasicTypesView>().First());

            void RunAssertions(BasicTypesView result)
            {
                Assert.Equal(source.Guid, result.Guid);
                Assert.Equal(source.Guid, result.Guid);
                Assert.Equal(source.DateTime, result.DateTime);
                Assert.Equal(StubEnum.Two, result.Enum);
                Assert.True(result.Bool);
                Assert.Equal('C', result.Char);
                Assert.Equal("S", result.String);
                Assert.Equal(1, result.Byte);
                Assert.Equal(2, result.Decimal);
                Assert.Equal(3, result.Double);
                Assert.Equal(4, result.Float);
                Assert.Equal(5, result.Int);
                Assert.Equal(6, result.Long);
                Assert.Equal(7, result.Object);
                Assert.Equal(8, result.Sbyte);
                Assert.Equal(9, result.Short);
                Assert.Equal(10u, result.Uint);
                Assert.Equal(11ul, result.Ulong);
                Assert.Equal(12, result.Ushort);
            };
        }
    }
}
