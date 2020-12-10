using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Spotify.Web.Authorization.Flows
{
    /// <summary>
    /// Represents a <see cref="SpotifyAuthorizationFlow"/> for applications where it's unsafe to store your client secret.
    /// Examples of these would include desktop apps, mobile apps, etc.
    /// </summary>
    public class AuthorizationCodeFlowWithPkce : SpotifyAuthorizationFlow
    {
        private readonly string code;
        private readonly string codeVerifier;
        private readonly string redirectUri;

        public AuthorizationCodeFlowWithPkce(
            HttpClient httpClient,
            string clientId,
            string code,
            string codeVerifier,
            string redirectUri) :
            base(httpClient, clientId)
        {
            this.code = code;
            this.codeVerifier = codeVerifier;
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
        /// <param name="codeChallenge">A <see cref="string"/> representing a code challenge. See <see cref="CreateCodeVerifierAndChallenge"/>.</param>
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
            string codeChallenge,
            string? state = null,
            AuthorizationScopes? scopes = null,
            bool? showDialog = null)
        {
            return new SpotifyUriBuilder(SpotifyAuthorizationFlow.AuthorizationUrl)
                .AppendToQuery("client_id", clientId)
                .AppendToQuery("response_type", "code")
                .AppendToQuery("redirect_uri", redirectUri)
                .AppendToQuery("code_challenge_method", "S256")
                .AppendToQuery("code_challenge", codeChallenge)
                .AppendToQueryIfNotNull("state", state)
                .AppendJoinToQueryIfNotNull("scope", "%20", scopes?.ToSpotifyStrings())
                .AppendToQueryIfNotNull("show_dialog", showDialog)
                .Build();
        }

        /// <summary>
        /// Creates a random code verifier and corresponding code challenge for use with an <see cref="AuthorizationCodeFlowWithPkce"/>.
        /// </summary>
        /// <returns>A <see cref="ValueTuple{T1, T2}"/> containing the code verifier and code challenge.</returns>
        public static (string CodeVerifier, string CodeChallenge) CreateCodeVerifierAndChallenge()
        {
            using var generator = RandomNumberGenerator.Create();

            var bytes = new byte[32];

            generator.GetBytes(bytes);

            var codeVerifier = Convert.ToBase64String(bytes)
                .TrimEnd('=')
                .Replace('+', '-')
                .Replace('/', '_');

            using var sha256 = SHA256.Create();

            var codeChallenge = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(codeVerifier)))
                .TrimEnd('=')
                .Replace('+', '-')
                .Replace('/', '_');

            return (codeVerifier, codeChallenge);
        }

        /// <inheritdoc/>
        public override async ValueTask<AccessToken> GetAccessTokenAsync(CancellationToken cancellationToken = default)
        {
            async Task GetAndStoreTokenAsync(HttpContent content)
            {
                var token = await base.GetAccessRefreshTokenAsync(content, null, cancellationToken);
                this.CurrentRefreshToken = token.RefreshToken ?? this.CurrentRefreshToken;
                base.CurrentAccessToken = token.AccessToken;
            }

            if (base.CurrentAccessToken is null)
            {
                using var content = new FormUrlEncodedContent(
                    new KeyValuePair<string?, string?>[]
                    {
                        new("grant_type", "authorization_code"),
                        new("code", this.code),
                        new("code_verifier", this.codeVerifier),
                        new("client_id", base.ClientId),
                        new("redirect_uri", this.redirectUri)
                    });

                await GetAndStoreTokenAsync(content).ConfigureAwait(false);
            }
            else if (base.CurrentAccessToken.Value.HasExpired)
            {
                if (this.CurrentRefreshToken is null)
                {
                    throw new InvalidOperationException("No refresh token to refresh access token with.");
                }

                using var content = new FormUrlEncodedContent(
                    new KeyValuePair<string?, string?>[]
                    {
                        new("grant_type", "refresh_token"),
                        new("refresh_token", this.CurrentRefreshToken),
                        new("client_id", base.ClientId)
                    });

                await GetAndStoreTokenAsync(content).ConfigureAwait(false);
            }

            return base.CurrentAccessToken!.Value;
        }
    }
}