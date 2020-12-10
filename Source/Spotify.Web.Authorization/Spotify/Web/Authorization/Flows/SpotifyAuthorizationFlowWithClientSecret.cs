using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Spotify.Web.Authorization.Flows
{
    /// <summary>
    /// Represents an <see href="https://spotify.dev/documentation/general/guides/authorization-guide/#authorization-flows">authorization flow</see>
    /// for the Spotify Web API that uses an application's secret key.
    /// </summary>
    public abstract class SpotifyAuthorizationFlowWithClientSecret : SpotifyAuthorizationFlow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpotifyAuthorizationFlowWithClientSecret"/> class with the
        /// specified <paramref name="httpClient"/>, <paramref name="clientId"/>, and <paramref name="clientSecret"/>.
        /// </summary>
        /// <param name="httpClient">
        /// An <see cref="HttpClient"/> instance to use to make requests to the Spotify Accounts service.
        /// </param>
        /// <param name="clientId">A <see cref="string"/> representing a valid Spotify Web API client ID.</param>
        /// <param name="clientSecret">
        /// A <see cref="string"/> representing the secret key of the application with the specified <paramref name="clientId"/>.
        /// </param>
        protected SpotifyAuthorizationFlowWithClientSecret(HttpClient httpClient, string clientId, string clientSecret) : base(httpClient, clientId)
        {
            BasicAuthenticationHeader = new("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{clientId}:{clientSecret}")));
        }

        /// <summary>
        /// Gets the 'Basic' <see cref="AuthenticationHeaderValue"/> used to make requests to the Spotify Accounts service.
        /// </summary>
        /// <returns>The 'Basic' <see cref="AuthenticationHeaderValue"/> used to make requests to the Spotify Accounts service.</returns>
        protected AuthenticationHeaderValue BasicAuthenticationHeader { get; }
    }
}