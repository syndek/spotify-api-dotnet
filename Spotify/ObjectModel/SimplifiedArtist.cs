using System;
using System.Collections.Generic;

namespace Spotify.ObjectModel
{
    /// <summary>
    /// Represents a simplified view of an <see cref="Artist"/>.
    /// </summary>
    public record SimplifiedArtist : LocatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SimplifiedArtist"/> class with the specified values.
        /// </summary>
        /// <param name="id">A <see cref="String"/> representing the Spotify ID for the artist.</param>
        /// <param name="uri">A <see cref="String"/> representing the Spotify URI for the artist.</param>
        /// <param name="href">
        /// A <see cref="String"/> representing a link to the Spotify Web API endpoint providing full details of the artist.
        /// </param>
        /// <param name="name">A <see cref="String"/> representing the name of the artist.</param>
        /// <param name="externalUrls">
        /// A <see cref="IReadOnlyDictionary{TKey, TValue}"/> containing the known
        /// external URLs for the artist, keyed by the type of the URL.
        /// </param>
        internal SimplifiedArtist(String id, Uri uri, Uri href, String name, IReadOnlyDictionary<String, Uri> externalUrls) : base(id, uri)
        {
            this.Href = href;
            this.Name = name;
            this.ExternalUrls = externalUrls;
        }

        /// <summary>
        /// Gets or sets a link to the Spotify Web API endpoint providing full details of <see cref="Artist"/>.
        /// </summary>
        /// <returns>A link to the Spotify Web API endpoint providing full details of the <see cref="Artist"/>.</returns>
        public Uri Href { get; init; }
        /// <summary>
        /// Gets or sets the name of the <see cref="Artist"/>.
        /// </summary>
        /// <returns>A <see cref="String"/> representing the name of the <see cref="Artist"/>.</returns>
        public String Name { get; init; }
        /// <summary>
        /// Gets or sets the known external URLs for the <see cref="Artist"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="IReadOnlyDictionary{TKey, TValue}"/> containing the known
        /// external URLs for the <see cref="Artist"/>, keyed by the type of the URL.
        /// </returns>
        public IReadOnlyDictionary<String, Uri> ExternalUrls { get; init; }
    }
}