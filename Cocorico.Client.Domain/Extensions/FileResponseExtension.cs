using Cocorico.Client.Domain.Helpers;

namespace Cocorico.Client.Domain.Extensions
{
    public static class FileResponseExtension
    {
        public static bool IsSuccessfulStatusCode(this FileResponse fileResponse) =>
            fileResponse.StatusCode == 200;
    }
}
