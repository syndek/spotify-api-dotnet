using System;

namespace Spotify.Web.Authorization
{
    /// <summary>
    /// Represents data about a Spotify access token.
    /// </summary>
    public readonly struct AccessToken
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccessToken"/> structure with the specified values.
        /// </summary>
        /// <param name="value">A <see cref="string"/> representing the value of the access token.</param>
        /// <param name="scope">The <see cref="AuthorizationScopes"/> the access token is valid for.</param>
        /// <param name="expiresIn">The number of seconds after issue at which the access token will expire.</param>
        public AccessToken(string value, AuthorizationScopes scope, int expiresIn)
        {
            Value = value;
            Scope = scope;
            ExpiresAt = DateTime.UtcNow.AddSeconds(expiresIn);
        }

        /// <summary>
        /// Gets the value of the <see cref="AccessToken"/>.
        /// </summary>
        /// <returns>A <see cref="string"/> representing the value of the <see cref="AccessToken"/>.</returns>
        public string Value { get; }
        /// <summary>
        /// Gets the <see cref="AuthorizationScopes"/> the <see cref="AccessToken"/> is valid for.
        /// </summary>
        /// <returns>The <see cref="AuthorizationScopes"/> the <see cref="AccessToken"/> is valid for.</returns>
        public AuthorizationScopes Scope { get; }
        /// <summary>
        /// Gets the <see cref="DateTime"/> at which the <see cref="AccessToken"/> will expire.
        /// </summary>
        /// <returns>The <see cref="DateTime"/> at which the <see cref="AccessToken"/> will expire.</returns>
        public DateTime ExpiresAt { get; }
        /// <summary>
        /// Gets a value indicating whether or not the <see cref="AccessToken"/> has expired.
        /// </summary>
        /// <returns><see langword="true"/> if the <see cref="AccessToken"/> has expired; otherwise, <see langword="false"/>.</returns>
        public bool HasExpired => DateTime.UtcNow >= ExpiresAt;
    }
}
