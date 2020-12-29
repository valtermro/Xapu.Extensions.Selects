using System.Collections.Generic;
using System.Linq;
using Xapu.Extensions.Selects.Tests.Base;
using Xapu.Extensions.Selects.Tests.FixtureTypes;
using Xunit;

namespace Xapu.Extensions.Selects.Tests.SelectFieldsTests
{
    public class CollectionTypeFields
    {
        [Fact]
        public void CollectionOfObjects()
        {
            var create = Creator.New(() => new CollectionsOfObjects
            {
                ObjectArray = new[]
                {
                    new ObjectType
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
                    }
                },
                ObjectIEnumerable = new List<ObjectType>
                {
                    new ObjectType
                    {
                        Id = 4,
                        Parent = new ObjectType
                        {
                            Id = 5,
                            Parent = new ObjectType
                            {
                                Id = 6,
                                Parent = new ObjectType()
                            }
                        }
                    }
                }
            });

            var fields = new[]
            {
                "ObjectArray.Id", "ObjectArray.Parent.Id", "ObjectArray.Parent.Parent.Id",
                "ObjectIEnumerable.Id", "ObjectIEnumerable.Parent.Id", "ObjectIEnumerable.Parent.Parent.Id"
            };

            static void Assertions(dynamic result)
            {
                var array = (dynamic[])result.ObjectArray;
                var enumerable = (IEnumerable<dynamic>)result.ObjectIEnumerable;

                Assert.Equal(1, array.First().Id);
                Assert.Equal(2, array.First().Parent.Id);
                Assert.Equal(3, array.First().Parent.Parent.Id);
                Assert.Equal(4, enumerable.First().Id);
                Assert.Equal(5, enumerable.First().Parent.Id);
                Assert.Equal(6, enumerable.First().Parent.Parent.Id);
            };

            Assertions(create.Array().SelectFields(fields).First());
            Assertions(create.Queryable().SelectFields(fields).First());
        }
    }
}
