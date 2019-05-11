using System;
using Cocorico.Shared.Exceptions;

namespace Cocorico.Server.Restful.Extensions
{
    public static class ExceptionExtension
    {
        public static int GetStatusCode(this Exception e)
        {
            switch (e)
            {
                case InvalidOperationException _: return 400;
                case EntityNotFoundException _: return 404;
                case UnexpectedException _: return 500;
                default: return 500;
            }
        }
    }
}
