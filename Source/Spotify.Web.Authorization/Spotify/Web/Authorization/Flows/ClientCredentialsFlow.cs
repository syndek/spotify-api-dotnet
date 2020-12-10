using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Spotify.Web.Authorization.Flows
{
    /// <summary>
    /// Represents a <see cref="SpotifyAuthorizationFlow"/> for accessing endpoints that do not access user information.
    /// </summary>
    /// <remarks>
    /// To be able to access user information, an <see cref="AuthorizationCodeFlow"/> or <see cref="AuthorizationCodeFlowWithPkce"/> should be used instead.
    /// A <see cref="ClientCredentialsFlow"/> does not include user authorization and therefore cannot be used to access user information.
    /// However, it does allow for a higher rate limit than requests made without an access token.
    /// </remarks>
    /// <seealso cref="AuthorizationCodeFlow"/>
    public class ClientCredentialsFlow : SpotifyAuthorizationFlowWithClientSecret
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientCredentialsFlow"/> class with the specified values.
        /// </summary>
        /// <param name="httpClient">An <see cref="HttpClient"/> instance to use to make requests to the Spotify Accounts service.</param>
        /// <param name="clientId">A <see cref="string"/> representing a valid Spotify Web API client ID.</param>
        /// <param name="clientSecret">
        /// A <see cref="string"/> representing the secret key of the application with the specified <paramref name="clientId"/>.
        /// </param>
        public ClientCredentialsFlow(HttpClient httpClient, string clientId, string clientSecret) : base(httpClient, clientId, clientSecret) { }

        /// <inheritdoc/>
        public override async ValueTask<AccessToken> GetAccessTokenAsync(CancellationToken cancellationToken = default)
        {
            if (base.CurrentAccessToken?.HasExpired is false)
            {
                return base.CurrentAccessToken.Value;
            }

            using var content = new FormUrlEncodedContent(
                new KeyValuePair<string?, string?>[]
                {
                    new("grant_type", "client_credentials")
                });

            var token = await base.GetAccessRefreshTokenAsync(content, base.BasicAuthenticationHeader, cancellationToken);

            base.CurrentAccessToken = token.AccessToken;

            return token.AccessToken;
        }
    }
}