using System;
using System.Collections.Generic;

namespace Cocorico.Server.Extensions
{
    public static class ExceptionExtension
    {
        public static int GetStatusCode(this Exception e)
        {
            switch (e)
            {
                case InvalidOperationException _:
                    return 400;
                case UnauthorizedAccessException _:
                    return 403;
                case KeyNotFoundException _:
                    return 404;
                default:
                    return 500;
            }
        }
    }
}
