using System.Linq;
using Xapu.Extensions.Selects.Tests.Base;
using Xapu.Extensions.Selects.Tests.FixtureTypes;
using Xunit;

namespace Xapu.Extensions.Selects.Tests.SelectFieldsTests
{
    public class ObjectTypeFields
    {
        [Fact]
        public void ShallowObject()
        {
            var create = Creator.New(() => new ObjectType
            {
                Id = 1,
                Parent = new ObjectType
                {
                    Id = 2,
                    Parent = new ObjectType()
                }
            });

            var fields = new[] { "Id", "Parent.Id" };

            static void Assertions(dynamic result)
            {
                Assert.Equal(1, result.Id);
                Assert.Equal(2, result.Parent.Id);
            };

            Assertions(create.Array().SelectFields(fields).First());
            Assertions(create.Queryable().SelectFields(fields).First());
        }

        [Fact]
        public void NestedObject()
        {
            var create = Creator.New(() => new ObjectType
            {
                Id = 1,
                Parent = new ObjectType
                {
                    Id = 2,
                    Parent = new ObjectType
                    {
                        Id = 3,
                        Parent = new ObjectType()
                    }
                }
            });

            var fields = new[] { "Id", "Parent.Id", "Parent.Parent.Id" };

            static void Assertions(dynamic result)
            {
                Assert.Equal(1, result.Id);
                Assert.Equal(2, result.Parent.Id);
                Assert.Equal(3, result.Parent.Parent.Id);
            };

            Assertions(create.Array().SelectFields(fields).First());
            Assertions(create.Queryable().SelectFields(fields).First());
        }
    }
}
