using System;

namespace Spotify.ObjectModel
{
    /// <summary>
    /// Represents an authentication error.
    /// </summary>
    internal record AuthenticationError : SpotifyObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationError"/> record with
        /// the specified <paramref name="error"/> and <paramref name="errorDescription"/>.
        /// </summary>
        /// <param name="error">
        /// A <see cref="String"/> representing a high level description of the error as specified in
        /// <see href="https://tools.ietf.org/html/rfc6749#section-5.2">RFC 6749 Section 5.2</see>.
        /// </param>
        /// <param name="errorDescription">
        /// A <see cref="String"/> representing a more detailed description of the error as specified
        /// in <see href="https://tools.ietf.org/html/rfc6749#section-4.1.2.1">RFC 6749 Section 4.1.2.1</see>.
        /// </param>
        internal AuthenticationError(String error, String? errorDescription) : base()
        {
            this.Error = error;
            this.ErrorDescription = errorDescription;
        }

        /// <summary>
        /// Gets or sets a high level description of the <see cref="AuthenticationError"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="String"/> representing a high level description of the error as specified in
        /// <see href="https://tools.ietf.org/html/rfc6749#section-5.2">RFC 6749 Section 5.2</see>.
        /// </returns>
        internal String Error { get; init; }
        /// <summary>
        /// Gets or sets a more detailed description of the <see cref="AuthenticationError"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="String"/> representing a more detailed description of the error as specified
        /// in <see href="https://tools.ietf.org/html/rfc6749#section-4.1.2.1">RFC 6749 Section 4.1.2.1</see>.
        /// </returns>
        internal String? ErrorDescription { get; init; }
    }
}