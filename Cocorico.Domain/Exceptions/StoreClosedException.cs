using System;
using System.Runtime.Serialization;

namespace Cocorico.Domain.Exceptions
{
    public sealed class StoreClosedException : Exception
    {
        public StoreClosedException(string message) : base(message)
        {
        }

        public StoreClosedException()
        {
        }

        public StoreClosedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public StoreClosedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
