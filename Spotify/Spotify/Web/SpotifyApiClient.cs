using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

using Spotify.ObjectModel;
using Spotify.Web.Authorization;

namespace Spotify.Web
{
    /// <summary>
    /// Represents a collection of endpoints defined by the Spotify Web API that share a common purpose.
    /// </summary>
    public abstract class SpotifyApiClient : Object, IDisposable
    {
        internal const String BaseUrl = "https://api.spotify.com/v1";

        private readonly HttpClient httpClient;
        private AuthenticationHeaderValue? currentAuthenticationHeader;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpotifyApiClient"/> class with the specified <paramref name="httpClient"/>.
        /// </summary>
        /// <param name="httpClient">An instance of <see cref="HttpClient"/> to use for requests to the Spotify Web API.</param>
        internal SpotifyApiClient(HttpClient httpClient) : base()
        {
            this.httpClient = httpClient;
        }

        /// <summary>
        /// Gets or sets the default <see cref="IAccessTokenProvider"/> to use in API requests if one is not passed to a given request method.
        /// </summary>
        /// <returns>The default <see cref="IAccessTokenProvider"/> to use in API requests.</returns>
        public IAccessTokenProvider? DefaultAccessTokenProvider { get; set; }

        /// <summary>
        /// Disposes the <see cref="HttpClient"/> being used to make requests to the Spotify Web API.
        /// </summary>
        public void Dispose()
        {
            this.httpClient.Dispose();
            GC.SuppressFinalize(this);
        }

        protected async Task<TObject> SendAsync<TObject>(
            Uri uri,
            HttpMethod method,
            HttpContent? content,
            IAccessTokenProvider? accessTokenProvider,
            CancellationToken cancellationToken)
        {
            using var message = await this
                .CreateAuthenticatedHttpRequestMessageAsync(uri, method, content, accessTokenProvider, cancellationToken)
                .ConfigureAwait(false);

            return await this.httpClient
                .SendMessageAsync<TObject, Error>(message, cancellationToken)
                .ConfigureAwait(false);
        }

        protected async Task SendAsync(
            Uri uri,
            HttpMethod method,
            HttpContent? content,
            IAccessTokenProvider? accessTokenProvider,
            CancellationToken cancellationToken)
        {
            using var message = await this
                .CreateAuthenticatedHttpRequestMessageAsync(uri, method, content, accessTokenProvider, cancellationToken)
                .ConfigureAwait(false);

            await this.httpClient
                .SendMessageAsync<Error>(message, cancellationToken)
                .ConfigureAwait(false);
        }

        private async ValueTask<HttpRequestMessage> CreateAuthenticatedHttpRequestMessageAsync(
            Uri uri,
            HttpMethod method,
            HttpContent? content,
            IAccessTokenProvider? accessTokenProvider,
            CancellationToken cancellationToken)
        {
            var message = new HttpRequestMessage(method, uri) { Content = content };

            // Refresh current access token if necessary.
            var provider = accessTokenProvider ??
                this.DefaultAccessTokenProvider ??
                throw new InvalidOperationException($"No {nameof(IAccessTokenProvider)} provided to acquire access token from.");

            var accessToken = await provider.GetAccessTokenAsync(cancellationToken).ConfigureAwait(false);

            if (this.currentAuthenticationHeader?.Parameter != accessToken.Value)
            {
                // Cache an instance of AuthenticationHeaderValue so one doesn't need to be created every time a request is made.
                this.currentAuthenticationHeader = new("Bearer", accessToken.Value);
            }

            message.Headers.Authorization = this.currentAuthenticationHeader;
            return message;
        }
    }
}