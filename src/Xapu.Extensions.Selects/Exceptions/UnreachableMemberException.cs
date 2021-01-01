using System;

namespace Xapu.Extensions.Selects
{
    [Serializable]
    public class UnreachableMemberException : Exception
    {
        public string MemberName { get; private set; }

        public UnreachableMemberException(string memberName)
            : base($"Member '{memberName}' is not accessible")
        {
            MemberName = memberName;
        }
    }
}