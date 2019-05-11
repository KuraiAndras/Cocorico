using System;

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
    }

    public class EntityNotFoundException : CocoricoException
    {
        public EntityNotFoundException()
        {
        }

        public EntityNotFoundException(string message) : base(message)
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
    }

    public class InvalidCredentialsException : CocoricoException
    {
        public InvalidCredentialsException()
        {
        }

        public InvalidCredentialsException(string message) : base(message)
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
    }
}
