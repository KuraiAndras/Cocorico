namespace Cocorico.HttpClient.Extensions
{
    public static class FileResponseExtension
    {
        public static bool IsSuccessfulStatusCode(this FileResponse fileResponse) =>
            fileResponse.StatusCode == 200;
    }
}
