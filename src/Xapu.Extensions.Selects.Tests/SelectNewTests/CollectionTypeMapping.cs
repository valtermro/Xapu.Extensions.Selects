using System;
using System.Collections.Generic;
using System.Linq;
using Xapu.Extensions.Selects.Tests.Base;
using Xapu.Extensions.Selects.Tests.FixtureTypes;
using Xunit;

namespace Xapu.Extensions.Selects.Tests.SelectNewTests
{
    public class CollectionTypeMapping
    {
        private static readonly Type ArrayIteratorT = EnumerableUtils.GetArrayIteratorType<int>();
        private static readonly Type ListIteratorT = EnumerableUtils.GetListIteratorType<int>();
        private static readonly Type ArrayIntT = typeof(int[]);
        private static readonly Type ListIntT = typeof(List<int>);

        [Fact]
        public void CollectionsOfBasicValues()
        {
            var create = Creator.New(() => new CollectionsOfBasicValues
            {
                IntArray = new int[] { 1, 2 },
                IntList = new List<int> { 3, 4 },
                IntIList = new List<int> { 5, 6 },
                IntICollection = new List<int> { 7, 8 },
                IntIEnumerable = new List<int> { 9, 10 }
            });

            static void Assertions(dynamic result, Type arrayResultType, Type listResultType)
            {
                Assert.Equal(new int[] { 1, 2 }, AssertX.Type(result.IntArray, arrayResultType));
                Assert.Equal(new int[] { 3, 4 }, AssertX.Type(result.IntList, listResultType));
                Assert.Equal(new int[] { 5, 6 }, AssertX.Type(result.IntIList, listResultType));
                Assert.Equal(new int[] { 7, 8 }, AssertX.Type(result.IntICollection, listResultType));
                Assert.Equal(new int[] { 9, 10 }, AssertX.Type(result.IntIEnumerable, listResultType));
            }

            Assertions(create.Object().ToNew<CollectionsToArrays>(), ArrayIntT, ArrayIntT);
            Assertions(create.Array().SelectNew<CollectionsToArrays>().First(), ArrayIntT, ArrayIntT);
            Assertions(create.Queryable().SelectNew<CollectionsToArrays>().First(), ArrayIntT, ArrayIntT);

            Assertions(create.Object().ToNew<CollectionsToLists>(), ListIntT, ListIntT);
            Assertions(create.Array().SelectNew<CollectionsToLists>().First(), ListIntT, ListIntT);
            Assertions(create.Queryable().SelectNew<CollectionsToLists>().First(), ListIntT, ListIntT);

            Assertions(create.Object().ToNew<CollectionsToILists>(), ListIntT, ListIntT);
            Assertions(create.Array().SelectNew<CollectionsToILists>().First(), ListIntT, ListIntT);
            Assertions(create.Queryable().SelectNew<CollectionsToILists>().First(), ListIntT, ListIntT);

            Assertions(create.Object().ToNew<CollectionsToICollections>(), ListIntT, ListIntT);
            Assertions(create.Array().SelectNew<CollectionsToICollections>().First(), ListIntT, ListIntT);
            Assertions(create.Queryable().SelectNew<CollectionsToICollections>().First(), ListIntT, ListIntT);

            Assertions(create.Object().ToNew<CollectionsToIEnumerables>(), ArrayIteratorT, ListIteratorT);
            Assertions(create.Array().SelectNew<CollectionsToIEnumerables>().First(), ArrayIteratorT, ListIteratorT);
            Assertions(create.Queryable().SelectNew<CollectionsToIEnumerables>().First(), ArrayIteratorT, ListIteratorT);
        }

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
                }
            });

            static void Assertions(CollectionsOfObjectViews result)
            {
                var resultItem = result.ObjectArray.First();

                Assert.Equal(1, resultItem.Id);
                Assert.Equal(2, resultItem.Parent.Id);
                Assert.Equal(3, resultItem.Parent.Parent.Id);
            };

            Assertions(create.Object().ToNew<CollectionsOfObjectViews>());
            Assertions(create.Array().SelectNew<CollectionsOfObjectViews>().First());
            Assertions(create.Queryable().SelectNew<CollectionsOfObjectViews>().First());
        }

        [Fact]
        public void CollectionOfNullableValuesToNonNullable()
        {
            var create = Creator.New(() => new CollectionsOfNullableValues
            {
                IntArray = new int?[] { 1, 2 }
            });

            static void Assertions(dynamic result)
            {
                Assert.Equal(new int[] { 1, 2 }, AssertX.Type(result.IntArray, ArrayIntT));
            }

            Assertions(create.Object().ToNew<CollectionsToArrays>());
        }

        [Fact]
        public void CollectionOfNonNullableValuesToNullable()
        {
            var create = Creator.New(() => new CollectionsOfBasicValues
            {
                IntArray = new int[] { 1, 2 }
            });

            static void Assertions(dynamic result)
            {
                Assert.Equal(new int?[] { 1, 2 }, AssertX.Type(result.IntArray, typeof(int?[])));
            }

            Assertions(create.Object().ToNew<CollectionsOfNullableValues>());
        }
    }
}
