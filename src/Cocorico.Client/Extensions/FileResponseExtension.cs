using Cocorico.Client.HttpClient;

namespace Cocorico.Client.Extensions
{
    public static class FileResponseExtension
    {
        public static bool IsSuccessfulStatusCode(this FileResponse fileResponse) =>
            fileResponse.StatusCode == 200;
    }
}
