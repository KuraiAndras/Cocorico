﻿using Cocorico.Client.Helpers;
using Cocorico.Shared.Exceptions;
using Cocorico.Shared.Helpers;
using Cocorico.Shared.Services.Helpers;
using Microsoft.JSInterop;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Cocorico.Client.Extensions
{
    public static class HttpClientExtension
    {
        public static async Task<IServiceResult<TTarget>> RetrieveFromServerAsync<TSource, TTarget>(
            this HttpClient httpClient,
            HttpVerbs verb,
            string requestUri,
            TSource body,
            CocoricoException exception = null)
            where TTarget : class
            where TSource : class
        {
            exception = exception ?? new UnexpectedException();

            HttpResponseMessage response;
            switch (verb)
            {
                case HttpVerbs.Post:
                    response = await httpClient.PostJsonWithResultAsync(requestUri, body);
                    break;
                case HttpVerbs.Get:
                    response = await httpClient.GetAsync(requestUri);
                    break;
                default: return new Fail<TTarget>(new InvalidCommandException());
            }

            if (!response.IsSuccessStatusCode) return new Fail<TTarget>(exception);

            var responseContent = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(responseContent)) return new Fail<TTarget>(new UnexpectedException());

            var resultDto = Json.Deserialize<TTarget>(responseContent);
            if (resultDto is null) return new Fail<TTarget>(new UnexpectedException());

            return new Success<TTarget>(resultDto);
        }

        public static async Task<IServiceResult<T>> RetrieveFromServerAsync<T>(
            this HttpClient httpClient,
            HttpVerbs verb,
            string requestUri,
            CocoricoException exception = null)
            where T : class =>
            await httpClient.RetrieveFromServerAsync<string, T>(verb, requestUri, "", exception);


        public static Task<HttpResponseMessage> PostJsonWithResultAsync(this HttpClient httpClient, string requestUri, object content) =>
            httpClient.PostAsync(requestUri, new StringContent(Json.Serialize(content), Encoding.UTF8, Verbs.ApplicationJson));
    }
}
