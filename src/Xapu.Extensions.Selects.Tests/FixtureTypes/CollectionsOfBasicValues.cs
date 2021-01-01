using System.Collections.Generic;

namespace Xapu.Extensions.Selects.Tests
{
    public class CollectionsOfBasicValues
    {
        public int[] IntArray { get; set; }
        public List<int> IntList { get; set; }
        public IList<int> IntIList { get; set; }
        public ICollection<int> IntICollection { get; set; }
        public IEnumerable<int> IntIEnumerable { get; set; }
    }

    public class CollectionsOfNullableValues
    {
        public int?[] IntArray { get; set; }
    }

    public class CollectionsToArrays
    {
        public int[] IntArray { get; set; }
        public int[] IntList { get; set; }
        public int[] IntIList { get; set; }
        public int[] IntICollection { get; set; }
        public int[] IntIEnumerable { get; set; }
    }

    public class CollectionsToLists
    {
        public List<int> IntArray { get; set; }
        public List<int> IntList { get; set; }
        public List<int> IntIList { get; set; }
        public List<int> IntICollection { get; set; }
        public List<int> IntIEnumerable { get; set; }
    }

    public class CollectionsToILists
    {
        public IList<int> IntArray { get; set; }
        public IList<int> IntList { get; set; }
        public IList<int> IntIList { get; set; }
        public IList<int> IntICollection { get; set; }
        public IList<int> IntIEnumerable { get; set; }
    }

    public class CollectionsToICollections
    {
        public ICollection<int> IntArray { get; set; }
        public ICollection<int> IntList { get; set; }
        public ICollection<int> IntIList { get; set; }
        public ICollection<int> IntICollection { get; set; }
        public ICollection<int> IntIEnumerable { get; set; }
    }

    public class CollectionsToIEnumerables
    {
        public IEnumerable<int> IntArray { get; set; }
        public IEnumerable<int> IntList { get; set; }
        public IEnumerable<int> IntIList { get; set; }
        public IEnumerable<int> IntICollection { get; set; }
        public IEnumerable<int> IntIEnumerable { get; set; }
    }
}
