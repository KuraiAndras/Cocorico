using Cocorico.Shared.Helpers;
using Microsoft.JSInterop;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Cocorico.Client.Extensions
{
    public static class HttpClientExtension
    {
        public static Task<HttpResponseMessage> PostJsonWithResultAsync(this HttpClient httpClient, string requestUri, object content) =>
            httpClient.PostAsync(requestUri, new StringContent(Json.Serialize(content), Encoding.UTF8, Verbs.ApplicationJson));
    }
}
