using Spotify.ObjectModel.Collections;
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
        /// Initializes a new instance of the <see cref="SimplifiedArtist"/> record with the specified values.
        /// </summary>
        /// <param name="id">A <see cref="string"/> representing the Spotify ID of the artist.</param>
        /// <param name="uri">The Spotify URI of the artist.</param>
        /// <param name="href">A link to the Spotify Web API endpoint providing full details of the artist.</param>
        /// <param name="name">A <see cref="string"/> representing the name of the artist.</param>
        /// <param name="externalUrls">
        /// A <see cref="IReadOnlyDictionary{TKey, TValue}"/> containing the known
        /// external URLs for the artist, keyed by the type of the URL.
        /// </param>
        public SimplifiedArtist(string id, Uri uri, Uri href, string name, IReadOnlyDictionary<string, Uri> externalUrls) : base(id, uri)
        {
            Href = href;
            Name = name;
            ExternalUrls = new ImmutableValueDictionary<string, Uri>(externalUrls);
        }

        /// <summary>
        /// Gets or sets a link to the Spotify Web API endpoint providing full details of <see cref="SimplifiedArtist"/>.
        /// </summary>
        /// <returns>A link to the Spotify Web API endpoint providing full details of the <see cref="SimplifiedArtist"/>.</returns>
        public Uri Href { get; init; }
        /// <summary>
        /// Gets or sets the name of the <see cref="SimplifiedArtist"/>.
        /// </summary>
        /// <returns>A <see cref="string"/> representing the name of the <see cref="SimplifiedArtist"/>.</returns>
        public string Name { get; init; }
        /// <summary>
        /// Gets or sets the known external URLs for the <see cref="SimplifiedArtist"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="IReadOnlyDictionary{TKey, TValue}"/> containing the known
        /// external URLs for the <see cref="SimplifiedArtist"/>, keyed by the type of the URL.
        /// </returns>
        public IReadOnlyDictionary<string, Uri> ExternalUrls { get; init; }
    }
}