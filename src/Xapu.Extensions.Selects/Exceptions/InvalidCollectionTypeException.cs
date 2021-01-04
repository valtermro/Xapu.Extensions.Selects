using System;

namespace Xapu.Extensions.Selects
{
    [Serializable]
    public class InvalidCollectionTypeException : Exception
    {
        public InvalidCollectionTypeException(Type type)
            : base($"{type} is not a valid collection type")
        {
        }
    }
}