using System.Linq;
using Xunit;

namespace Xapu.Extensions.Selects.Tests.SelectFieldsTests
{
    public class UnreachableMembers
    {
        [Fact]
        public void ThrowsIfMemberIsNotPublic()
        {
            var create = Creator.New(() => new NoAccessibleGetters
            {
                Int = 42
            });

            Assert.Throws<UnreachableMemberException>(() => create.Array().SelectFields("Int").ToList());
            Assert.Throws<UnreachableMemberException>(() => create.Queryable().SelectFields("Int").ToList());
        }

        [Fact]
        public void ThrowsIfMemberDoesNotExist()
        {
            var create = Creator.New(() => new NoAccessibleGetters
            {
                Int = 42
            });

            Assert.Throws<UnreachableMemberException>(() => create.Array().SelectFields("Int").ToList());
            Assert.Throws<UnreachableMemberException>(() => create.Queryable().SelectFields("Int").ToList());
        }
    }
}
