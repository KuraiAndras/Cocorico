using System;
using System.Runtime.Serialization;

namespace Cocorico.Domain.Exceptions
{
    public sealed class InvalidCommandException : Exception
    {
        public InvalidCommandException()
        {
        }

        public InvalidCommandException(string message) : base(message)
        {
        }

        public InvalidCommandException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public InvalidCommandException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}