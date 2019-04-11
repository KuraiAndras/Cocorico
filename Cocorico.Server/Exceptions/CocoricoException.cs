using System;

namespace Cocorico.Server.Exceptions
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
}
