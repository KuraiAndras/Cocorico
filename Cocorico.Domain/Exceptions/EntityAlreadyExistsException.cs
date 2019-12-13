using System;
using System.Runtime.Serialization;

namespace Cocorico.Domain.Exceptions
{
    public sealed class EntityAlreadyExistsException : Exception
    {
        public EntityAlreadyExistsException()
        {
        }

        public EntityAlreadyExistsException(string message) : base(message)
        {
        }

        public EntityAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public EntityAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}