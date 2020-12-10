using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using Spotify.Web.Authorization.Serialization;

namespace Spotify.Web.Authorization.Flows
{
    /// <summary>
    /// Represents an <see href="https://spotify.dev/documentation/general/guides/authorization-guide/#authorization-flows">authorization flow</see>
    /// for the Spotify Web API.
    /// </summary>
    public abstract class SpotifyAuthorizationFlow : object, IAccessTokenProvider, IDisposable
    {
        /// <summary>
        /// Represents the URL of the <c>/authorize</c> endpoint of the Spotify Accounts service. This field is constant.
        /// </summary>
        protected const string AuthorizationUrl = "https://accounts.spotify.com/authorize";

        /// <summary>
        /// The <see cref="Uri"/> of the Spotify Accounts service <c>/api/token</c> endpoint. This field is read-only.
        /// </summary>
        private static readonly Uri TokenUri = new("https://accounts.spotify.com/api/token");
        private static readonly JsonSerializerOptions AccessTokenSerializerOptions = new()
        {
            Converters =
            {
                new AccessTokenConverter(),
                new AccessRefreshTokenConverter()
            }
        };
        private static readonly JsonSerializerOptions AuthenticationErrorSerializerOptions = new()
        {
            Converters = { new AuthenticationErrorConverter() }
        };

        private readonly HttpClient httpClient;

        private bool isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpotifyAuthorizationFlow"/> class with the
        /// specified <paramref name="httpClient"/> and <paramref name="clientId"/>.
        /// </summary>
        /// <param name="httpClient">
        /// An <see cref="System.Net.Http.HttpClient"/> instance to use to make requests to the Spotify Accounts service.
        /// </param>
        /// <param name="clientId">A <see cref="string"/> representing a valid Spotify Web API client ID.</param>
        protected SpotifyAuthorizationFlow(HttpClient httpClient, string clientId) : base()
        {
            this.isDisposed = false;
            this.httpClient = httpClient;
            this.ClientId = clientId;
        }

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="SpotifyAuthorizationFlow"/>.
        /// </summary>
        ~SpotifyAuthorizationFlow() => this.Dispose(false);

        /// <summary>
        /// Gets or sets the current <see cref="AccessToken"/> being used by the <see cref="SpotifyAuthorizationFlow"/> instance.
        /// </summary>
        /// <returns>The current <see cref="AccessToken"/>, or <see langword="null"/> if none have been acquired yet.</returns>
        public AccessToken? CurrentAccessToken { get; protected set; }

        /// <summary>
        /// Gets the client ID of the application the <see cref="SpotifyAuthorizationFlow"/> is for.
        /// </summary>
        /// <returns>A <see cref="string"/> representing the client ID of the application the <see cref="SpotifyAuthorizationFlow"/> is for.</returns>
        protected string ClientId { get; }

        /// <summary>
        /// Asynchronously retrieves an <see cref="AccessToken"/>.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests.</param>
        /// <remarks>
        /// This method may either use an existing <see cref="AccessToken"/>, or if it has expired,
        /// request and cache a new <see cref="AccessToken"/> from the Spotify Accounts service.
        /// </remarks>
        /// <returns>A <see cref="ValueTask{TResult}"/> representing the asynchronous operation.</returns>
        public abstract ValueTask<AccessToken> GetAccessTokenAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Releases all resources used by the <see cref="SpotifyAuthorizationFlow"/>.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="SpotifyAuthorizationFlow"/> and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">
        /// <see langword="true"/> to release both managed and unmanaged resources; <see langword="false"/> to release only unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (this.isDisposed)
            {
                return;
            }

            if (disposing)
            {
                this.httpClient.Dispose();
            }

            this.isDisposed = true;
        }

        protected async Task<AccessRefreshToken> GetAccessRefreshTokenAsync(
            HttpContent content,
            AuthenticationHeaderValue? authenticationHeader,
            CancellationToken cancellationToken)
        {
            using var message = new HttpRequestMessage(HttpMethod.Post, SpotifyAuthorizationFlow.TokenUri)
            {
                Content = content,
                Headers = { Authorization = authenticationHeader }
            };

            using var response = await this.httpClient
                .SendAsync(message, cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content
                    .ReadFromJsonAsync<AuthenticationError>(SpotifyAuthorizationFlow.AuthenticationErrorSerializerOptions, cancellationToken)
                    .ConfigureAwait(false);

                throw new HttpRequestException(error.ToString(), null, response.StatusCode);
            }

            var token = await response.Content
                .ReadFromJsonAsync<AccessRefreshToken>(SpotifyAuthorizationFlow.AccessTokenSerializerOptions, cancellationToken)
                .ConfigureAwait(false);

            return token;
        }
    }
}