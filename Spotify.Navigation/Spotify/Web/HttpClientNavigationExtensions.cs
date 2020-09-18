using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Spotify.ObjectModel;
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

            return await httpClient
                .SendMessageAsync<TObject, Error>(message, cancellationToken)
                .ConfigureAwait(false);
        }
    }
}