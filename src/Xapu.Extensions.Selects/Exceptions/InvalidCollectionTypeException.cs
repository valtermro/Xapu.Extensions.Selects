using System;
using System.Runtime.Serialization;

namespace Xapu.Extensions.Selects.Exceptions
{
    [Serializable]
    public class InvalidCollectionTypeException : Exception
    {
        public InvalidCollectionTypeException(Type type)
            : this($"{type} is not a valid collection type")
        {
        }

        public InvalidCollectionTypeException(string message)
            : base(message)
        {
        }

        public InvalidCollectionTypeException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected InvalidCollectionTypeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}