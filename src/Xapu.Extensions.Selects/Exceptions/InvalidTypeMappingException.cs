﻿using System;

namespace Xapu.Extensions.Selects
{
    [Serializable]
    public class InvalidTypeMappingException : Exception
    {
        public InvalidTypeMappingException(Type sourceType, Type resultType)
            : base($"Cannot map from {sourceType} to {resultType}")
        {
        }
    }
}