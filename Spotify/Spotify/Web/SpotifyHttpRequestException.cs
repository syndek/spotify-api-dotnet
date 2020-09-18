using System;
using System.Net;
using System.Net.Http;

namespace Spotify.Web
{
    /// <summary>
    /// The exception that is thrown when a request to the Spotify Web API results in an error.
    /// </summary>
    public class SpotifyHttpRequestException : HttpRequestException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpotifyHttpRequestException"/> class
        /// with the specified <paramref name="statusCode"/> and <paramref name="error"/>.
        /// </summary>
        /// <param name="statusCode">The <see cref="HttpStatusCode"/> of the response.</param>
        /// <param name="error">A <see cref="String"/> representing the error.</param>
        internal SpotifyHttpRequestException(HttpStatusCode statusCode, String error) : base(error, null, statusCode) { }
    }
}