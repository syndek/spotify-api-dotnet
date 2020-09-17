using System;
using System.Net;

namespace Spotify.Web.Authorization
{
    /// <summary>
    /// The exception that is thrown when an error occurs during authorization with the Spotify Accounts service.
    /// </summary>
    public class SpotifyAuthorizationException : SpotifyHttpRequestException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpotifyAuthorizationException"/> class with the specified values.
        /// </summary>
        /// <param name="statusCode">The <see cref="HttpStatusCode"/> of the response.</param>
        /// <param name="error">A <see cref="String"/> representing the error.</param>
        /// <param name="errorDescription">
        /// A <see cref="String"/> representing a description of the error, or <see langword="null"/> if none was provided.
        /// </param>
        internal SpotifyAuthorizationException(HttpStatusCode statusCode, String error, String? errorDescription) :
            base(statusCode, error + (errorDescription is null ? String.Empty : $": {errorDescription}"))
        { }
    }
}