namespace Spotify.Web.Authorization
{
    /// <summary>
    /// Represents an authentication error.
    /// </summary>
    internal readonly struct AuthenticationError
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationError"/> structure with
        /// the specified <paramref name="error"/> and <paramref name="errorDescription"/>.
        /// </summary>
        /// <param name="error">
        /// A <see cref="string"/> representing a high level description of the error as specified in
        /// <see href="https://tools.ietf.org/html/rfc6749#section-5.2">RFC 6749 Section 5.2</see>.
        /// </param>
        /// <param name="errorDescription">
        /// A <see cref="string"/> representing a more detailed description of the error as specified
        /// in <see href="https://tools.ietf.org/html/rfc6749#section-4.1.2.1">RFC 6749 Section 4.1.2.1</see>.
        /// </param>
        internal AuthenticationError(string error, string? errorDescription)
        {
            Error = error;
            ErrorDescription = errorDescription;
        }

        /// <summary>
        /// Gets a high level description of the <see cref="AuthenticationError"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="string"/> representing a high level description of the error as specified in
        /// <see href="https://tools.ietf.org/html/rfc6749#section-5.2">RFC 6749 Section 5.2</see>.
        /// </returns>
        internal string Error { get; }
        /// <summary>
        /// Gets a detailed description of the <see cref="AuthenticationError"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="string"/> representing a detailed description of the error as specified
        /// in <see href="https://tools.ietf.org/html/rfc6749#section-4.1.2.1">RFC 6749 Section 4.1.2.1</see>.
        /// </returns>
        internal string? ErrorDescription { get; }

        /// <summary>
        /// Returns a <see cref="string"/> representing the <see cref="AuthenticationError"/>.
        /// </summary>
        /// <returns>A <see cref="string"/> representing the <see cref="AuthenticationError"/>.</returns>
        public override string ToString() => Error + (ErrorDescription is null ? string.Empty : ": " + ErrorDescription);
    }
}