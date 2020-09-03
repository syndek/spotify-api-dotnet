using System;
using System.Collections.Generic;

namespace Spotify.ObjectModel
{
    /// <summary>
    /// Represents the context from which a <see cref="Track"/> is played.
    /// </summary>
    public record Context : SpotifyObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Context"/> record with the specified values.
        /// </summary>
        /// <param name="uri">A <see cref="String"/> representing the Spotify URI of the context.</param>
        /// <param name="href">
        /// A <see cref="String"/> representing a link to the Spotify Web API endpoint providing full details of
        /// the <see cref="Track"/>.
        /// </param>
        /// <param name="externalUrls">
        /// A <see cref="IReadOnlyDictionary{TKey,TValue}"/> containing the known external URLs for the
        /// context, keyed by the type of the URL.
        /// </param>
        internal Context(Uri uri, Uri href, IReadOnlyDictionary<String, Uri> externalUrls) : base()
        {
            this.Uri = uri;
            this.Href = href;
            this.ExternalUrls = externalUrls;
        }

        /// <summary>
        /// Gets or sets the <see href="https://developer.spotify.com/documentation/web-api/#spotify-uris-and-ids">Spotify URI</see>
        /// for the <see cref="Context"/>.
        /// </summary>
        /// <returns>A <see cref="String"/> representing the Spotify URI of the context.</returns>
        public Uri Uri { get; init; }
        /// <summary>
        /// Gets or sets a link to the Spotify Web API endpoint providing full details of <see cref="Track"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="String"/> representing a link to the Spotify Web API endpoint providing full details of the <see cref="Track"/>.
        /// </returns>
        public Uri Href { get; init; }
        /// <summary>
        /// Gets or sets the known external URLs for the <see cref="Context"/>
        /// </summary>
        /// <returns>
        /// A <see cref="IReadOnlyDictionary{TKey, TValue}"/> containing the known external URLs for the
        /// <see cref="Context"/>, keyed by the type of the URL.
        /// </returns>
        public IReadOnlyDictionary<String, Uri> ExternalUrls { get; init; }
    }
}