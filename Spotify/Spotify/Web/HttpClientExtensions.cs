using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using Spotify.ObjectModel;
using Spotify.ObjectModel.JsonConverters;
using Spotify.Web.Authorization;

namespace Spotify.Web
{
    internal static class HttpClientExtensions : Object
    {
        private static readonly JsonSerializerOptions ErrorSerializerOptions = new();

        static HttpClientExtensions()
        {
            var converters = HttpClientExtensions.ErrorSerializerOptions.Converters;

            converters.Add(ErrorConverter.Instance);
            converters.Add(AuthenticationErrorConverter.Instance);
        }

        internal static async Task<TObject> SendMessageAsync<TObject, TError>(
            this HttpClient client,
            HttpRequestMessage message,
            CancellationToken cancellationToken)
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

            var result = await response.Content
                .ReadFromJsonAsync<TObject>(ConverterHelpers.JsonSerializerOptions, cancellationToken)
                .ConfigureAwait(false);

            return result ?? throw new InvalidOperationException($"Failed to deserialize response to an instance of {typeof(TObject).Name}.");
        }

        internal static async Task SendMessageAsync<TError>(
            this HttpClient client,
            HttpRequestMessage message,
            CancellationToken cancellationToken)
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

        private static async Task<HttpRequestException> ConstructExceptionAsync<TError>(
            HttpContent content,
            HttpStatusCode statusCode,
            CancellationToken cancellationToken)
        {
            var errorObject = await content
                .ReadFromJsonAsync<TError>(HttpClientExtensions.ErrorSerializerOptions, cancellationToken)
                .ConfigureAwait(false);

            throw errorObject switch
            {
                AuthenticationError error => new SpotifyAuthorizationException(statusCode, error.Error, error.ErrorDescription),
                Error error => new SpotifyHttpRequestException(statusCode, error.Message),
                _ => new HttpRequestException($"Unknown error: {errorObject}", null, statusCode)
            };
        }
    }
}