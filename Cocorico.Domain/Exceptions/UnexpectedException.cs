using System;
using System.Runtime.Serialization;

namespace Cocorico.Domain.Exceptions
{
    public sealed class UnexpectedException : Exception
    {
        public UnexpectedException()
        {
        }

        public UnexpectedException(string message) : base(message)
        {
        }

        public UnexpectedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public UnexpectedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}