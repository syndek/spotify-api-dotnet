using System;

namespace Spotify.ObjectModel
{
    /// <summary>
    /// Represents an <see cref="IdentifiableObject"/> with a designated
    /// <see href="https://spotify.dev/documentation/web-api/#spotify-uris-and-ids">Spotify URI</see>.
    /// </summary>
    public record LocatableObject : IdentifiableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocatableObject"/> record
        /// with the specified <paramref name="id"/> and <paramref name="uri"/>.
        /// </summary>
        /// <param name="id">A <see cref="string"/> representing the Spotify ID of the object.</param>
        /// <param name="uri">The Spotify URI of the object.</param>
        public LocatableObject(string id, Uri uri) : base(id)
        {
            Uri = uri;
        }

        /// <summary>
        /// Gets or sets the <see href="https://spotify.dev/documentation/web-api/#spotify-uris-and-ids">Spotify URI</see>
        /// of the <see cref="LocatableObject"/>.
        /// </summary>
        /// <returns>The Spotify URI of the <see cref="LocatableObject"/>.</returns>
        public Uri Uri { get; init; }
    }
}
