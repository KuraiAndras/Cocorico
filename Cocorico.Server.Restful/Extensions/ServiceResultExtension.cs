using Cocorico.Shared.Services.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Cocorico.Server.Restful.Extensions
{
    public static class ServiceResultExtension
    {
        public static IActionResult ToActionResult(this IServiceResult serviceResult)
        {
            switch (serviceResult)
            {
                case Success _: return new OkResult();
                case Fail fail: return new StatusCodeResult(fail.Exception.GetStatusCode());
                default: return new BadRequestResult();
            }
        }

        public static IActionResult ToActionResult<T>(this IServiceResult<T> serviceResult)
        {
            switch (serviceResult)
            {
                case Success<T> success: return new JsonResult(success.Data);
                case Fail<T> fail: return new StatusCodeResult(fail.Exception.GetStatusCode());
                default: return new BadRequestResult();
            }
        }
    }
}
