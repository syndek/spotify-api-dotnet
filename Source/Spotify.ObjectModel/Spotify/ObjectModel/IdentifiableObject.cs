namespace Spotify.ObjectModel
{
    /// <summary>
    /// Represents a <see cref="SpotifyObject"/> with a unique
    /// <see href="https://spotify.dev/documentation/web-api/#spotify-uris-and-ids">Spotify ID</see>.
    /// </summary>
    public abstract record IdentifiableObject : SpotifyObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IdentifiableObject"/> record with the specified <paramref name="id"/>.
        /// </summary>
        /// <param name="id">A <see cref="string"/> representing the Spotify ID of the object.</param>
        public IdentifiableObject(string id) : base()
        {
            Id = id;
        }

        /// <summary>
        /// Gets or sets the <see href="https://spotify.dev/documentation/web-api/#spotify-uris-and-ids">Spotify ID</see>
        /// of the <see cref="IdentifiableObject"/>.
        /// </summary>
        /// <returns>A <see cref="string"/> representing the Spotify ID of the <see cref="IdentifiableObject"/>.</returns>
        public string Id { get; init; }
    }
}