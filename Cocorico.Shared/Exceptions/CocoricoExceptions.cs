using System;
using System.Runtime.Serialization;

namespace Cocorico.Shared.Exceptions
{
    public abstract class CocoricoException : Exception
    {
        protected CocoricoException(string message) : base(message)
        {
        }

        protected CocoricoException()
        {
        }

        protected CocoricoException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CocoricoException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    public class EntityNotFoundException : CocoricoException
    {
        public EntityNotFoundException()
        {
        }

        public EntityNotFoundException(string message) : base(message)
        {
        }

        public EntityNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public EntityNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    public class UnexpectedException : CocoricoException
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

    public class InvalidCredentialsException : CocoricoException
    {
        public InvalidCredentialsException()
        {
        }

        public InvalidCredentialsException(string message) : base(message)
        {
        }

        public InvalidCredentialsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public InvalidCredentialsException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    public class InvalidCommandException : CocoricoException
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

    public class EntityAlreadyExistsException : CocoricoException
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

    public class StoreClosedException : CocoricoException
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
