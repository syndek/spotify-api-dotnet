using System;

namespace Spotify.Web.Authorization
{
    /// <summary>
    /// The exception that is thrown when an error occurrs during authorization with the Spotify Accounts service.
    /// </summary>
    internal class SpotifyAuthorizationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpotifyAuthorizationException"/> class
        /// with the specified <paramref name="error"/> and <paramref name="errorDescription"/>.
        /// </summary>
        /// <param name="error">A <see cref="String"/> representing the error.</param>
        /// <param name="errorDescription">
        /// A <see cref="String"/> representing a description of the error, or <see langword="null"/> if none was provided.
        /// </param>
        internal SpotifyAuthorizationException(String error, String? errorDescription) :
            base($"{error}: {errorDescription ?? "No description"}")
        { }
    }
}