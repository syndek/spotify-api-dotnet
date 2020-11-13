using System;
using System.Net.Http;
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
    public abstract class SpotifyAuthorizationFlow : Object, IAccessTokenProvider, IDisposable
    {
        /// <summary>
        /// Represents the URL of the <c>/authorize</c> endpoint of the Spotify Accounts service. This field is constant.
        /// </summary>
        protected const String AuthorizationUrl = "https://accounts.spotify.com/authorize";
        
        /// <summary>
        /// The <see cref="Uri"/> of the Spotify Accounts service <c>/api/token</c> endpoint. This field is read-only.
        /// </summary>
        protected static readonly Uri TokenUri = new("https://accounts.spotify.com/api/token");

        protected static readonly JsonSerializerOptions AccessTokenSerializerOptions = new()
        {
            Converters =
            {
                new AccessTokenConverter(),
                new AccessRefreshTokenConverter()
            }
        };

        protected static readonly JsonSerializerOptions AuthenticationErrorSerializerOptions = new()
        {
            Converters = { new AuthenticationErrorConverter() }
        };

        private Boolean isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpotifyAuthorizationFlow"/> class with the
        /// specified <paramref name="httpClient"/> and <paramref name="clientId"/>.
        /// </summary>
        /// <param name="httpClient">
        /// An <see cref="System.Net.Http.HttpClient"/> instance to use to make requests to the Spotify Accounts service.
        /// </param>
        /// <param name="clientId">A <see cref="String"/> representing a valid Spotify Web API client ID.</param>
        protected SpotifyAuthorizationFlow(HttpClient httpClient, String clientId) : base()
        {
            this.isDisposed = false;
            this.HttpClient = httpClient;
            this.ClientId = clientId;
        }

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="SpotifyAuthorizationFlow"/>.
        /// </summary>
        ~SpotifyAuthorizationFlow() => this.Dispose(false);

        /// <summary>
        /// Gets the <see cref="System.Net.Http.HttpClient"/> being used to make requests to the Spotify Accounts service.
        /// </summary>
        /// <returns>The <see cref="System.Net.Http.HttpClient"/> being used to make requests to the Spotify Accounts service.</returns>
        protected HttpClient HttpClient { get; }
        /// <summary>
        /// Gets the client ID of the application the <see cref="SpotifyAuthorizationFlow"/> is for.
        /// </summary>
        /// <returns>A <see cref="String"/> representing the client ID of the application the <see cref="SpotifyAuthorizationFlow"/> is for.</returns>
        protected String ClientId { get; }
        /// <summary>
        /// Gets or sets the current <see cref="AccessToken"/> being used by the <see cref="SpotifyAuthorizationFlow"/> instance.
        /// </summary>
        /// <returns>The current <see cref="AccessToken"/>, or <see langword="null"/> if none have been acquired yet.</returns>
        protected AccessToken? CurrentAccessToken { get; set; }

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
        protected virtual void Dispose(Boolean disposing)
        {
            if (this.isDisposed)
            {
                return;
            }

            if (disposing)
            {
                this.HttpClient.Dispose();
            }

            this.isDisposed = true;
        }
    }
}