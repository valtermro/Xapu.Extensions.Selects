using System;
using System.Collections.Generic;
using System.Linq;
using Xapu.Extensions.Selects.Tests.Base;
using Xapu.Extensions.Selects.Tests.FixtureTypes;
using Xunit;

namespace Xapu.Extensions.Selects.Tests
{
    public class CollectionTypeMapping
    {
        private readonly Type TypeOfArraySelectIterator = new int[] { 1 }.Select(p => p).GetType();
        private readonly Type TypeOfSelectListIterator = new List<int>().Select(p => p).GetType();

        [Fact]
        public void CollectionsOfBasicValues()
        {
            var source = new CollectionsOfBasicValues
            {
                IntArray = new int[] { 1, 2 },
                IntList = new List<int> { 3, 4 },
                IntIList = new List<int> { 5, 6 },
                IntICollection = new List<int> { 7, 8 },
                IntIEnumerable = new List<int> { 9, 10 }
            };

            RunToArrayAssertions(source.ToNew<CollectionsToArrays>());
            RunToArrayAssertions(new[] { source }.SelectNew<CollectionsToArrays>().First());
            RunToArrayAssertions(new[] { source }.AsQueryable().SelectNew<CollectionsToArrays>().First());

            RunToListAssertions(source.ToNew<CollectionsToLists>());
            RunToListAssertions(new[] { source }.SelectNew<CollectionsToLists>().First());
            RunToListAssertions(new[] { source }.AsQueryable().SelectNew<CollectionsToLists>().First());

            RunToIListAssertions(source.ToNew<CollectionsToILists>());
            RunToIListAssertions(new[] { source }.SelectNew<CollectionsToILists>().First());
            RunToIListAssertions(new[] { source }.AsQueryable().SelectNew<CollectionsToILists>().First());

            RunToICollectionAssertions(source.ToNew<CollectionsToICollections>());
            RunToICollectionAssertions(new[] { source }.SelectNew<CollectionsToICollections>().First());
            RunToICollectionAssertions(new[] { source }.AsQueryable().SelectNew<CollectionsToICollections>().First());

            RunToIEnumerableAssertions(source.ToNew<CollectionsToIEnumerables>());
            RunToIEnumerableAssertions(new[] { source }.SelectNew<CollectionsToIEnumerables>().First());
            RunToIEnumerableAssertions(new[] { source }.AsQueryable().SelectNew<CollectionsToIEnumerables>().First());

            void RunToArrayAssertions(CollectionsToArrays result)
            {
                Assert.Equal(source.IntArray, AssertX.Type(result.IntArray, typeof(int[])));
                Assert.Equal(source.IntList, AssertX.Type(result.IntList, typeof(int[])));
                Assert.Equal(source.IntIList, AssertX.Type(result.IntIList, typeof(int[])));
                Assert.Equal(source.IntICollection, AssertX.Type(result.IntICollection, typeof(int[])));
                Assert.Equal(source.IntIEnumerable, AssertX.Type(result.IntIEnumerable, typeof(int[])));
            };

            void RunToListAssertions(CollectionsToLists result)
            {
                Assert.Equal(source.IntArray, AssertX.Type(result.IntArray, typeof(List<int>)));
                Assert.Equal(source.IntList, AssertX.Type(result.IntList, typeof(List<int>)));
                Assert.Equal(source.IntIList, AssertX.Type(result.IntIList, typeof(List<int>)));
                Assert.Equal(source.IntICollection, AssertX.Type(result.IntICollection, typeof(List<int>)));
                Assert.Equal(source.IntIEnumerable, AssertX.Type(result.IntIEnumerable, typeof(List<int>)));
            };

            void RunToIListAssertions(CollectionsToILists result)
            {
                Assert.Equal(source.IntArray, AssertX.Type(result.IntArray, typeof(List<int>)));
                Assert.Equal(source.IntList, AssertX.Type(result.IntList, typeof(List<int>)));
                Assert.Equal(source.IntIList, AssertX.Type(result.IntIList, typeof(List<int>)));
                Assert.Equal(source.IntICollection, AssertX.Type(result.IntICollection, typeof(List<int>)));
                Assert.Equal(source.IntIEnumerable, AssertX.Type(result.IntIEnumerable, typeof(List<int>)));
            };

            void RunToICollectionAssertions(CollectionsToICollections result)
            {
                Assert.Equal(source.IntArray, AssertX.Type(result.IntArray, typeof(List<int>)));
                Assert.Equal(source.IntList, AssertX.Type(result.IntList, typeof(List<int>)));
                Assert.Equal(source.IntIList, AssertX.Type(result.IntIList, typeof(List<int>)));
                Assert.Equal(source.IntICollection, AssertX.Type(result.IntICollection, typeof(List<int>)));
                Assert.Equal(source.IntIEnumerable, AssertX.Type(result.IntIEnumerable, typeof(List<int>)));
            };

            void RunToIEnumerableAssertions(CollectionsToIEnumerables result)
            {
                Assert.Equal(source.IntArray, AssertX.Type(result.IntArray, TypeOfArraySelectIterator));
                Assert.Equal(source.IntList, AssertX.Type(result.IntList, TypeOfSelectListIterator));
                Assert.Equal(source.IntIList, AssertX.Type(result.IntIList, TypeOfSelectListIterator));
                Assert.Equal(source.IntICollection, AssertX.Type(result.IntICollection, TypeOfSelectListIterator));
                Assert.Equal(source.IntIEnumerable, AssertX.Type(result.IntIEnumerable, TypeOfSelectListIterator));
            };
        }

        [Fact]
        public void CollectionOfObjects()
        {
            var source = new CollectionsOfObjects
            {
                ObjectArray = new[]
                {
                    new ObjectType
                    {
                        Id = Guid.NewGuid(),
                        Parent = new ObjectType
                        {
                            Id = Guid.NewGuid(),
                            Parent = new ObjectType
                            {
                                Id = Guid.NewGuid(),
                                Parent = new ObjectType()
                            }
                        }
                    }
                }
            };

            RunAssertions(source.ToNew<CollectionsOfObjectViews>());
            RunAssertions(new[] { source }.SelectNew<CollectionsOfObjectViews>().First());
            RunAssertions(new[] { source }.AsQueryable().SelectNew<CollectionsOfObjectViews>().First());

            void RunAssertions(CollectionsOfObjectViews result)
            {
                var sourceItem = source.ObjectArray.First();
                var resultItem = result.ObjectArray.First();

                Assert.Equal(sourceItem.Id, resultItem.Id);
                Assert.Equal(sourceItem.Parent.Id, resultItem.Parent.Id);
                Assert.Equal(sourceItem.Parent.Parent.Id, resultItem.Parent.Parent.Id);
            };
        }
    }
}
