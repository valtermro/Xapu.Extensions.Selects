using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Xapu.Extensions.Selects.Tests.SelectFieldsTests
{
    public class ResultTypes
    {
        [Fact]
        public void EnumerableSelectReturnsIEnumerable()
        {
            var array = Creator.New(() => new ObjectType()).Array();

            var result = array.SelectFields("Id");

            AssertX.AssignableFrom(result, typeof(IEnumerable<object>));
        }

        [Fact]
        public void QueryableSelectReturnsIQueryable()
        {
            var query = Creator.New(() => new ObjectType()).Queryable();

            var result = query.SelectFields("Id");

            AssertX.AssignableFrom(result, typeof(IQueryable<object>));
        }
    }
}
