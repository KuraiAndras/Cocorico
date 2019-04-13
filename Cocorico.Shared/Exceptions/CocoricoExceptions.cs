using System;

namespace Cocorico.Shared.Exceptions
{
    public abstract class CocoricoException : Exception
    {
    }

    public class EntityNotFoundException : CocoricoException
    {
    }

    public class UnexpectedException : CocoricoException
    {
    }

    public class UnauthorizedException : CocoricoException
    {
    }

    public class DatabaseException : CocoricoException
    {
    }

    public class InvalidCredentialsException : CocoricoException
    {
    }

    public class InvalidCommandException : CocoricoException
    {
    }
}
