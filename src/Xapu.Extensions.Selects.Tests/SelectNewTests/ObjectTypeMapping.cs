using System.Linq;
using Xunit;

namespace Xapu.Extensions.Selects.Tests.SelectNewTests
{
    public class ObjectTypeMapping
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

            static void Assertions(ShallowObjectView result)
            {
                Assert.Equal(1, result.Id);
                Assert.Equal(2, result.Parent.Id);
            };

            Assertions(create.Object().ToNew<ShallowObjectView>());
            Assertions(create.Array().SelectNew<ShallowObjectView>().First());
            Assertions(create.Queryable().SelectNew<ShallowObjectView>().First());
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

            static void Assertions(NestedObjectView result)
            {
                Assert.Equal(1, result.Id);
                Assert.Equal(2, result.Parent.Id);
                Assert.Equal(3, result.Parent.Parent.Id);
            };

            Assertions(create.Object().ToNew<NestedObjectView>());
            Assertions(create.Array().SelectNew<NestedObjectView>().First());
            Assertions(create.Queryable().SelectNew<NestedObjectView>().First());
        }
    }
}
