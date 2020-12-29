using System;
using System.Collections.Generic;
using System.Linq;
using Xapu.Extensions.Selects.Tests.Base;
using Xapu.Extensions.Selects.Tests.FixtureTypes;
using Xunit;

namespace Xapu.Extensions.Selects.Tests.SelectFieldsTests
{
    public class AssocWithNoNestedFieldsListed
    {
        private static readonly Type ListIteratorT = EnumerableUtils.GetListIteratorType<int>();
        private static readonly Type ArrayIntT = typeof(int[]);
        private static readonly Type ListIntT = typeof(List<int>);

        [Fact]
        public void ObjectTypes()
        {
            var create = Creator.New(() => new NestedObjectView
            {
                Id = 1,
                Value = "01",
                Parent = new ShallowObjectView
                {
                    Id = 2,
                    Value = "02",
                    Parent = new WithValueView
                    {
                        Id = 3
                    }
                }
            });

            var fields = new[] { "Id", "Parent" };

            static void Assertions(dynamic result)
            {
                Assert.Equal(1, result.Id);
                Assert.Equal(2, result.Parent.Id);
                Assert.Equal("02", result.Parent.Value);
                Assert.Equal(3, result.Parent.Parent.Id);
                Assert.Null(result.Parent.Parent.Value);

                Assert.Null(result.GetType().GetField("Value"));
            };

            Assertions(create.Array().SelectFields(fields).First());
            Assertions(create.Queryable().SelectFields(fields).First());
        }

        [Fact]
        public void NestedFullObject()
        {
            var create = Creator.New(() => new NestedObjectView
            {
                Id = 1,
                Value = "01",
                Parent = new ShallowObjectView
                {
                    Id = 2,
                    Value = "02",
                    Parent = new WithValueView
                    {
                        Id = 3,
                        Value = "03"
                    }
                }
            });

            var fields = new[] { "Id", "Parent.Id", "Parent.Parent" };

            static void Assertions(dynamic result)
            {
                Assert.Equal(1, result.Id);
                Assert.Equal(2, result.Parent.Id);
                Assert.Equal(3, result.Parent.Parent.Id);
                Assert.Equal("03", result.Parent.Parent.Value);

                Assert.Null(result.GetType().GetField("Value"));
                Assert.Null(result.GetType().GetField("Parent").GetType().GetField("Value"));
            };

            Assertions(create.Array().SelectFields(fields).First());
            Assertions(create.Queryable().SelectFields(fields).First());
        }

        [Fact]
        public void NestedCollectionsOfBasicValues()
        {
            var create = Creator.New(() => new CollectionsOfBasicValues
            {
                IntArray = new int[] { 1, 2 },
                IntList = new List<int> { 3, 4 },
                IntIList = new List<int> { 5, 6 },
                IntICollection = new List<int> { 7, 8 },
                IntIEnumerable = new List<int> { 9, 10 }
            });

            var fields = new[] { "IntArray", "IntList", "IntIList", "IntICollection", "IntIEnumerable" };

            static void Assertions(dynamic result)
            {
                Assert.Equal(new int[] { 1, 2 }, AssertX.Type(result.IntArray, ArrayIntT));
                Assert.Equal(new int[] { 3, 4 }, AssertX.Type(result.IntList, ListIntT));
                Assert.Equal(new int[] { 5, 6 }, AssertX.Type(result.IntIList, ListIntT));
                Assert.Equal(new int[] { 7, 8 }, AssertX.Type(result.IntICollection, ListIntT));
                Assert.Equal(new int[] { 9, 10 }, AssertX.Type(result.IntIEnumerable, ListIteratorT));
            }

            Assertions(create.Array().SelectFields(fields).First());
            Assertions(create.Queryable().SelectFields(fields).First());

            Assertions(create.Array().SelectFields(fields).First());
            Assertions(create.Queryable().SelectFields(fields).First());

            Assertions(create.Array().SelectFields(fields).First());
            Assertions(create.Queryable().SelectFields(fields).First());
            
            Assertions(create.Array().SelectFields(fields).First());
            Assertions(create.Queryable().SelectFields(fields).First());

            Assertions(create.Array().SelectFields(fields).First());
            Assertions(create.Queryable().SelectFields(fields).First());
        }

        [Fact]
        public void NestedCollectionOfObjects()
        {
            var create = Creator.New(() => new CollectionsOfObjects<NestedObjectView>
            {
                ObjectArray = new[]
                {
                    new NestedObjectView
                    {
                        Id = 1,
                        Value = "01",
                        Parent = new ShallowObjectView
                        {
                            Id = 2,
                            Value = "02",
                            Parent = new WithValueView
                            {
                                Id = 3,
                                Value = "03"
                            }
                        }
                    }
                }
            });

            var fields = new[] { "ObjectArray" };

            static void Assertions(dynamic result)
            {
                var resultItem = result.ObjectArray[0];

                Assert.Equal(1, resultItem.Id);
                Assert.Equal("01", resultItem.Value);
                Assert.Equal(2, resultItem.Parent.Id);
                Assert.Equal("02", resultItem.Parent.Value);
                Assert.Equal(3, resultItem.Parent.Parent.Id);
                Assert.Equal("03", resultItem.Parent.Parent.Value);
            };

            Assertions(create.Array().SelectFields(fields).First());
            Assertions(create.Queryable().SelectFields(fields).First());
        }

        [Fact]
        public void NestedCollectionOfNullableValues()
        {
            var create = Creator.New(() => new CollectionsOfNullableValues
            {
                IntArray = new int?[] { 1, 2 }
            });

            var fields = new[] { "IntArray" };

            static void Assertions(dynamic result)
            {
                Assert.Equal(new int?[] { 1, 2 }, AssertX.Type(result.IntArray, typeof(int?[])));
            }

            Assertions(create.Array().SelectFields(fields).First());
        }
    }
}
