using System;

namespace Spotify.ObjectModel
{
    /// <summary>
    /// Represents an <see cref="IdentifiableObject"/> with a designated
    /// <see href="https://developer.spotify.com/documentation/web-api/#spotify-uris-and-ids">Spotify URI</see>.
    /// </summary>
    public record LocatableObject : IdentifiableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocatableObject"/> class
        /// with the specified <paramref name="id"/> and <paramref name="uri"/>.
        /// </summary>
        /// <param name="id">A <see cref="String"/> representing the Spotify ID for the object.</param>
        /// <param name="uri">The Spotify URI for the object.</param>
        public LocatableObject(String id, Uri uri) : base(id)
        {
            this.Uri = uri;
        }

        /// <summary>
        /// Gets or sets the <see href="https://developer.spotify.com/documentation/web-api/#spotify-uris-and-ids">Spotify URI</see>
        /// for the <see cref="LocatableObject"/>.
        /// </summary>
        /// <returns>The Spotify URI for the <see cref="LocatableObject"/>.</returns>
        public Uri Uri { get; init; }
    }
}