using System;
using Cocorico.Server.Exceptions;

namespace Cocorico.Server.Extensions
{
    public static class ExceptionExtension
    {
        public static int GetStatusCode(this Exception e)
        {
            switch (e)
            {
                case InvalidOperationException _: return 400;
                case UnauthorizedException _: return 403;
                case EntityNotFoundException _: return 404;
                case UnexpectedException _: return 500;
                default: return 500;
            }
        }
    }
}
