using System.Net.Http;
using Cocorico.Shared.Exceptions;
using Cocorico.Shared.Services.Helpers;

namespace Cocorico.Client.Extensions
{
    public static class HttpResponseMessageExtension
    {
        public static IServiceResult GetServiceResult(this HttpResponseMessage httpResponseMessage, CocoricoException exception = null) =>
            httpResponseMessage.IsSuccessStatusCode
                ? (IServiceResult) new Success()
                : new Fail(exception ?? new UnexpectedException());
    }
}
