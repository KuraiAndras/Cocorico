using System;
using System.Runtime.Serialization;

namespace Cocorico.Client.Domain.Exceptions
{
    public class ClientException : Exception
    {
        public ClientException()
        {
        }

        protected ClientException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public ClientException(string message) : base(message)
        {
        }

        public ClientException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    public class RegisterFailedException : Exception
    {
        public RegisterFailedException()
        {
        }

        protected RegisterFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public RegisterFailedException(string message) : base(message)
        {
        }

        public RegisterFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
