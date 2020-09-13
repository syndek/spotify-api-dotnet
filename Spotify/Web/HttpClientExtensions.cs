using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

using Spotify.ObjectModel;
using Spotify.ObjectModel.JsonConverters;
using Spotify.Web.Authorization;

namespace Spotify.Web
{
    internal static class HttpClientExtensions : Object
    {
        internal static async Task<TObject> SendMessageAsync<TObject, TError>(
            this HttpClient client,
            HttpRequestMessage message,
            CancellationToken cancellationToken = default)
        {
            using var response = await client
                .SendAsync(message, cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                throw await HttpClientExtensions
                    .ConstructExceptionAsync<TError>(response.Content, response.StatusCode, cancellationToken)
                    .ConfigureAwait(false);
            }

            return await response.Content
                .ReadFromJsonAsync<TObject>(ConverterHelpers.JsonSerializerOptions, cancellationToken)
                .ConfigureAwait(false);
        }

        internal static async Task SendMessageAsync<TError>(
            this HttpClient client,
            HttpRequestMessage message,
            CancellationToken cancellationToken = default)
        {
            using var response = await client
                .SendAsync(message, cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                throw await HttpClientExtensions
                    .ConstructExceptionAsync<TError>(response.Content, response.StatusCode, cancellationToken)
                    .ConfigureAwait(false);
            }
        }

        internal static async Task<TObject> GetAsync<TObject>(
            this HttpClient httpClient,
            Uri uri,
            IAccessTokenProvider accessTokenProvider,
            CancellationToken cancellationToken = default)
        {
            var accessToken = await accessTokenProvider
                .GetAccessTokenAsync(cancellationToken)
                .ConfigureAwait(false);

            using var message = new HttpRequestMessage(HttpMethod.Get, uri);
            message.Headers.Authorization = new("Bearer", accessToken.Value);

            return await httpClient
                .SendMessageAsync<TObject, Error>(message, cancellationToken)
                .ConfigureAwait(false);
        }

        private static async Task<HttpRequestException> ConstructExceptionAsync<TError>(
            HttpContent content,
            HttpStatusCode statusCode,
            CancellationToken cancellationToken = default)
        {
            var errorObject = await content
                .ReadFromJsonAsync<TError>(ConverterHelpers.JsonSerializerOptions, cancellationToken)
                .ConfigureAwait(false);

            throw errorObject switch
            {
                AuthenticationError error => new SpotifyAuthorizationException(error.Error, error.ErrorDescription, null, statusCode),
                Error error => new HttpRequestException(error.Message, null, statusCode),
                _ => new HttpRequestException($"Unknown error: {errorObject}", null, statusCode)
            };
        }
    }
}