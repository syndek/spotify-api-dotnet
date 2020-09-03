using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Spotify.Web.Authorization
{
    /// <summary>
    /// Represents an <see href="https://spotify.dev/documentation/general/guides/authorization-guide/#authorization-flows">authorization flow</see>
    /// for the Spotify Web API.
    /// </summary>
    public abstract class SpotifyAuthorizationFlow : Object, IAccessTokenProvider, IDisposable
    {
        /// <summary>
        /// The <see cref="Uri"/> of the Spotify Accounts service <c>/api/token</c> endpoint. This field is read-only.
        /// </summary>
        protected static readonly Uri TokenUri = new("https://accounts.spotify.com/api/token");

        /// <summary>
        /// Initializes a new instance of the <see cref="SpotifyAuthorizationFlow"/> class with the
        /// specified <paramref name="httpClient"/>, <paramref name="clientId"/>, and <paramref name="clientSecret"/>.
        /// </summary>
        /// <param name="httpClient">
        /// A <see cref="System.Net.Http.HttpClient"/> instance to use to make requests to the Spotify Accounts service.
        /// </param>
        /// <param name="clientId">A valid Spotify Web API client ID.</param>
        /// <param name="clientSecret">The secret key of the application with the specified client ID.</param>
        public SpotifyAuthorizationFlow(HttpClient httpClient, String clientId, String clientSecret) : base()
        {
            this.HttpClient = httpClient;
            this.BasicAuthenticationHeader = new("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{clientId}:{clientSecret}")));
        }

        /// <summary>
        /// Gets the <see cref="System.Net.Http.HttpClient"/> being used to make requests to the Spotify Accounts service.
        /// </summary>
        /// <returns>The <see cref="System.Net.Http.HttpClient"/> being used to make requests to the Spotify Accounts service.</returns>
        protected HttpClient HttpClient { get; }
        /// <summary>
        /// Gets the 'Basic' <see cref="AuthenticationHeaderValue"/> used to make requests to the Spotify Accounts service.
        /// </summary>
        /// <returns>The 'Basic' <see cref="AuthenticationHeaderValue"/> used to make requests to the Spotify Accounts service.</returns>
        protected AuthenticationHeaderValue BasicAuthenticationHeader { get; }
        /// <summary>
        /// Gets or sets the current <see cref="AccessToken"/> being used by the <see cref="SpotifyAuthorizationFlow"/> instance.
        /// </summary>
        /// <returns>The current <see cref="AccessToken"/>, or <see langword="null"/> if none have been acquired yet.</returns>
        protected AccessToken? CurrentAccessToken { get; set; }

        /// <summary>
        /// Asynchronously retrieves an <see cref="AccessToken"/>.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests.</param>
        /// <returns>A <see cref="ValueTask{TResult}"/> representing the asynchronous operation.</returns>
        /// <remarks>
        /// This method may either use an existing <see cref="AccessToken"/>, or if it has expired,
        /// request and cache a new <see cref="AccessToken"/> from the Spotify Accounts service.
        /// </remarks>
        public abstract ValueTask<AccessToken> GetAccessTokenAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Disposes the <see cref="System.Net.Http.HttpClient"/> being used to make requests to the Spotify Accounts service.
        /// </summary>
        public void Dispose()
        {
            this.HttpClient.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}