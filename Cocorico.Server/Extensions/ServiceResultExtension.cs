using Cocorico.Server.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Cocorico.Server.Extensions
{
    public static class ServiceResultExtension
    {
        public static IActionResult ToActionResult(this ServiceResult serviceResult)
        {
            return serviceResult.IsSuccessful ? new OkResult() : new StatusCodeResult(serviceResult.Exception.GetStatusCode());
        }

        public static IActionResult ToActionResult<T>(this ServiceResult<T> serviceResult)
        {
            return serviceResult.IsSuccessful ? (IActionResult)new JsonResult(serviceResult.Data) : new StatusCodeResult(serviceResult.Exception.GetStatusCode());
        }
    }
}
