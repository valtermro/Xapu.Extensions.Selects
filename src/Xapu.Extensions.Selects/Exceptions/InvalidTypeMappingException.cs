using System;
using System.Runtime.Serialization;

namespace Xapu.Extensions.Selects.Exceptions
{
    [Serializable]
    public class InvalidTypeMappingException : Exception
    {
        public InvalidTypeMappingException(Type sourceType, Type resultType)
            : this($"Cannot map from {sourceType} to {resultType}")
        {
        }

        public InvalidTypeMappingException(string message)
            : base(message)
        {
        }

        public InvalidTypeMappingException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected InvalidTypeMappingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}