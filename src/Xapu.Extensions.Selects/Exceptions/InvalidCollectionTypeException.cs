using System;
using System.Runtime.Serialization;

namespace Xapu.Extensions.Selects.Exceptions
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