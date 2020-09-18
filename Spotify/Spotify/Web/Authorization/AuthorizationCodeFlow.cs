using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Spotify.ObjectModel;
using Spotify.ObjectModel.EnumConverters;

namespace Spotify.Web.Authorization
{
    /// <summary>
    /// Represents a <see cref="SpotifyAuthorizationException"/> suitable for
    /// long-running applications in which a user grants permission only once.
    /// </summary>
    public class AuthorizationCodeFlow : SpotifyAuthorizationFlow
    {
        /// <summary>
        /// Represents the URL of the <c>/authorize</c> endpoint of the Spotify Accounts service. This field is constant.
        /// </summary>
        protected const String AuthorizationUrl = "https://accounts.spotify.com/authorize";

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationCodeFlow"/> class with the specified values.
        /// </summary>
        /// <param name="httpClient">A <see cref="HttpClient"/> instance to use to make requests to the Spotify Accounts service.</param>
        /// <param name="clientId">A <see cref="String"/> representing a valid Spotify Web API client ID.</param>
        /// <param name="clientSecret">
        /// A <see cref="String>"/> representing the secret key of the application with the specified client ID.
        /// </param>
        /// <param name="code">
        /// A <see cref="String"/> representing the authorization code returned from an initial request to the <c>/authorize</c> endpoint.
        /// </param>
        /// <param name="redirectUri">
        /// A <see cref="String"/> representing the redirect URI supplied in the initial request to the the <c>/authorize</c> endpoint.
        /// This parameter is used purely for validation and therefore must be an exact match. No actual redirection takes place.
        /// </param>
        public AuthorizationCodeFlow(
            HttpClient httpClient,
            String clientId,
            String clientSecret,
            String code,
            String redirectUri) :
            base(httpClient, clientId, clientSecret)
        {
            this.Code = code;
            this.RedirectUri = redirectUri;
        }

        /// <summary>
        /// The authorization code returned from an initial request to the <c>/authorize</c> endpoint.
        /// </summary>
        /// <returns>
        /// A <see cref="String"/> representing the authorization code returned from an initial request to the <c>/authorize</c> endpoint.
        /// </returns>
        protected String Code { get; }
        /// <summary>
        /// Gets the redirect URI supplied in the initial request to the the <c>/authorize</c> endpoint.
        /// </summary>
        /// <returns>
        /// A <see cref="String"/> representing the redirect URI supplied in the initial request to the the <c>/authorize</c> endpoint.
        /// </returns>
        protected String RedirectUri { get; }
        /// <summary>
        /// Gets or sets a token that can be used to refresh the <see cref="SpotifyAuthorizationFlow.CurrentAccessToken"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="String"/> representing a token that can be used to refresh the
        /// <see cref="SpotifyAuthorizationFlow.CurrentAccessToken"/>, or <see langword="null"/> if none was provided.
        /// </returns>
        protected String? RefreshToken { get; set; }

        /// <summary>
        /// Creates a <see cref="Uri"/> that can be used to allow a user to authorize an application.
        /// </summary>
        /// <param name="clientId">A <see cref="String"/> representing the Spotify Web API client ID of the application.</param>
        /// <param name="redirectUri">A <see cref="String"/> representing the URI to redirect to after the user grants or denies permission.</param>
        /// <param name="state">
        /// A <see cref="String"/> that can provide protection against attacks such as cross-site request forgery.
        /// See <see href="https://tools.ietf.org/html/rfc6749#section-4.1">RFC-6749</see>.
        /// Technically optional, but <i>strongly</i> recommended.
        /// </param>
        /// <param name="scopes">The <see cref="AuthorizationScopes"/> to authorize the application to use.</param>
        /// <param name="showDialog">Whether or not to force the user to approve the application again if they’ve already done so.</param>
        /// <returns>The created <see cref="Uri"/>.</returns>
        public static Uri CreateAuthorizationUrl(
            String clientId,
            String redirectUri,
            String? state = null,
            AuthorizationScopes? scopes = null,
            Boolean? showDialog = null)
        {
            return new SpotifyUriBuilder(AuthorizationCodeFlow.AuthorizationUrl)
                .AppendToQuery("client_id", clientId)
                .AppendToQuery("response_type", "code")
                .AppendToQuery("redirect_uri", redirectUri)
                .AppendToQueryIfNotNull("state", state)
                .AppendJoinToQueryIfNotNull("scope", "%20", scopes?.ToSpotifyStrings())
                .AppendToQueryIfNotNull("show_dialog", showDialog)
                .Build();
        }

        /// <inheritdoc/>
        public override async ValueTask<AccessToken> GetAccessTokenAsync(CancellationToken cancellationToken = default)
        {
            async Task GetAccessRefreshToken(HttpContent content)
            {
                using var message = new HttpRequestMessage(HttpMethod.Post, SpotifyAuthorizationFlow.TokenUri) { Content = content };
                message.Headers.Authorization = base.BasicAuthenticationHeader;

                var accessRefreshToken = await this.HttpClient.SendMessageAsync<AccessRefreshToken, AuthenticationError>(
                    message,
                    cancellationToken);

                base.CurrentAccessToken = accessRefreshToken.AccessToken;
                this.RefreshToken = accessRefreshToken.RefreshToken ?? this.RefreshToken;
            }

            if (base.CurrentAccessToken is null)
            {
                using var content = new StringContent(
                    $"grant_type=authorization_code&code={this.Code}&redirect_uri={this.RedirectUri}",
                    Encoding.UTF8,
                    "application/x-www-form-urlencoded");

                await GetAccessRefreshToken(content);
            }
            else if (base.CurrentAccessToken.Value.HasExpired)
            {
                if (this.RefreshToken is null)
                {
                    throw new InvalidOperationException("No refresh token to refresh access token with.");
                }

                using var content = new StringContent(
                    $"grant_type=refresh_token&refresh_token={this.RefreshToken}",
                    Encoding.UTF8,
                    "application/x-www-form-urlencoded");

                await GetAccessRefreshToken(content);
            }

            return base.CurrentAccessToken!.Value;
        }
    }
}