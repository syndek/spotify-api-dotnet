using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

using Spotify.Web.Authorization;

namespace Spotify.Web
{
    internal static class HttpClientNavigationExtensions : Object
    {
        internal static async Task<TObject> GetAsync<TObject>(
            this HttpClient httpClient,
            Uri uri,
            IAccessTokenProvider accessTokenProvider,
            CancellationToken cancellationToken)
        {
            var accessToken = await accessTokenProvider
                .GetAccessTokenAsync(cancellationToken)
                .ConfigureAwait(false);

            using var message = new HttpRequestMessage(HttpMethod.Get, uri);
            message.Headers.Authorization = new("Bearer", accessToken.Value);

            var response = await httpClient.SendAsync(message, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                var returned = await response.Content.ReadFromJsonAsync<TObject>(null, cancellationToken);
                return returned!;
            }
            else
            {
                var error = await response.Content.ReadFromJsonAsync<Error>(null, cancellationToken);
                throw new SpotifyHttpRequestException(response.StatusCode, error.Message);
            }
        }
    }
}