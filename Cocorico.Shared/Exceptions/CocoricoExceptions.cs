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
    }

    public class InvalidCredentialsException : CocoricoException
    {
    }

    public class InvalidCommandException : CocoricoException
    {
    }

    public class EntityAlreadyExistsException : CocoricoException
    {
    }
}
