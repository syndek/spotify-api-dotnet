using System;

namespace Spotify.ObjectModel
{
    /// <summary>
    /// Represents information about the followers of an entity within Spotify.
    /// </summary>
    public record Followers : SpotifyObject
    {
        /// <summary>
        /// Represents no followers. This field is read-only.
        /// </summary>
        public static readonly Followers None = new(null, 0);

        /// <summary>
        /// Initializes a new instance of the <see cref="Followers"/> record with the specified values.
        /// </summary>
        /// <param name="href">
        /// A link to the Spotify Web API endpoint providing full details of the followers,
        /// or <see langword="null"/> if unavailable.
        /// </param>
        /// <param name="total">The total number of followers.</param>
        public Followers(Uri? href, Int32 total) : base()
        {
            this.Href = href;
            this.Total = total;
        }

        /// <summary>
        /// Gets or sets a link to the Spotify Web API endpoint providing full details of the <see cref="Followers"/>.
        /// </summary>
        /// <remarks>Currently always <see langword="null"/>, as the API does not yet support it.</remarks>
        /// <returns>
        /// A link to the Spotify Web API endpoint providing full details of the <see cref="Followers"/>,
        /// or <see langword="null"/> if unavailable.
        /// </returns>
        public Uri? Href { get; init; }
        /// <summary>
        /// Gets or sets the total number of followers.
        /// </summary>
        /// <returns>The total number of followers.</returns>
        public Int32 Total { get; init; }
    }
}