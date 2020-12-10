using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Spotify.Web.Authorization.Flows
{
    /// <summary>
    /// Represents a <see cref="SpotifyAuthorizationFlow"/> suitable for
    /// long-running applications in which a user grants permission only once.
    /// </summary>
    public class AuthorizationCodeFlow : SpotifyAuthorizationFlowWithClientSecret
    {
        private readonly string code;
        private readonly string redirectUri;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationCodeFlow"/> class with the specified values.
        /// </summary>
        /// <param name="httpClient">An <see cref="HttpClient"/> instance to use to make requests to the Spotify Accounts service.</param>
        /// <param name="clientId">A <see cref="string"/> representing a valid Spotify Web API client ID.</param>
        /// <param name="clientSecret">
        /// A <see cref="string"/> representing the secret key of the application with the specified <paramref name="clientId"/>.
        /// </param>
        /// <param name="code">
        /// A <see cref="string"/> representing the authorization code returned from an initial request to the <c>/authorize</c> endpoint.
        /// </param>
        /// <param name="redirectUri">
        /// A <see cref="string"/> representing the redirect URI supplied in the initial request to the <c>/authorize</c> endpoint.
        /// This parameter is used purely for validation and therefore must be an exact match. No actual redirection takes place.
        /// </param>
        public AuthorizationCodeFlow(
            HttpClient httpClient,
            string clientId,
            string clientSecret,
            string code,
            string redirectUri) :
            base(httpClient, clientId, clientSecret)
        {
            this.code = code;
            this.redirectUri = redirectUri;
        }

        /// <summary>
        /// Gets or sets a token that can be used to refresh the <see cref="SpotifyAuthorizationFlow.CurrentAccessToken"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="string"/> representing a token that can be used to refresh the
        /// <see cref="SpotifyAuthorizationFlow.CurrentAccessToken"/>, or <see langword="null"/> if none was provided.
        /// </returns>
        public string? CurrentRefreshToken { get; private set; }

        /// <summary>
        /// Creates a <see cref="Uri"/> that can be used to allow a user to authorize an application.
        /// </summary>
        /// <param name="clientId">A <see cref="string"/> representing the Spotify Web API client ID of the application.</param>
        /// <param name="redirectUri">A <see cref="string"/> representing the URI to redirect to after the user grants or denies permission.</param>
        /// <param name="state">
        /// A <see cref="string"/> that can provide protection against attacks such as cross-site request forgery.
        /// See <see href="https://tools.ietf.org/html/rfc6749#section-4.1">RFC-6749</see>.
        /// Technically optional, but <i>strongly</i> recommended.
        /// </param>
        /// <param name="scopes">The <see cref="AuthorizationScopes"/> to authorize the application to use.</param>
        /// <param name="showDialog">Whether or not to force the user to approve the application again if they’ve already done so.</param>
        /// <returns>The created <see cref="Uri"/>.</returns>
        public static Uri CreateAuthorizationUri(
            string clientId,
            string redirectUri,
            string? state = null,
            AuthorizationScopes? scopes = null,
            bool? showDialog = null)
        {
            return new SpotifyUriBuilder(AuthorizationUrl)
                .AppendToQuery("response_type", "code")
                .AppendToQuery("client_id", clientId)
                .AppendToQuery("redirect_uri", redirectUri)
                .AppendToQueryIfNotNull("state", state)
                .AppendJoinToQueryIfNotNull("scope", "%20", scopes?.ToSpotifyStrings())
                .AppendToQueryIfNotNull("show_dialog", showDialog)
                .Build();
        }

        /// <inheritdoc/>
        public override async ValueTask<AccessToken> GetAccessTokenAsync(CancellationToken cancellationToken = default)
        {
            async Task GetAndStoreTokenAsync(HttpContent content)
            {
                var token = await GetAccessRefreshTokenAsync(content, BasicAuthenticationHeader, cancellationToken);
                CurrentRefreshToken = token.RefreshToken ?? CurrentRefreshToken;
                CurrentAccessToken = token.AccessToken;
            }

            if (CurrentAccessToken is null)
            {
                using var content = new FormUrlEncodedContent(
                    new KeyValuePair<string?, string?>[]
                    {
                        new("grant_type", "authorization_code"),
                        new("code", code),
                        new("redirect_uri", redirectUri)
                    });

                await GetAndStoreTokenAsync(content).ConfigureAwait(false);
            }
            else if (CurrentAccessToken.Value.HasExpired)
            {
                if (CurrentRefreshToken is null)
                {
                    throw new InvalidOperationException("No refresh token to refresh access token with.");
                }

                using var content = new FormUrlEncodedContent(
                    new KeyValuePair<string?, string?>[]
                    {
                        new("grant_type", "refresh_token"),
                        new("refresh_token", CurrentRefreshToken)
                    });

                await GetAndStoreTokenAsync(content).ConfigureAwait(false);
            }

            return CurrentAccessToken!.Value;
        }
    }
}