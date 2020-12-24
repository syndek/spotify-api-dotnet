using System;
using System.Collections.Generic;
using Spotify.ObjectModel.Collections;

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
        /// <param name="uri">The Spotify URI of the context.</param>
        /// <param name="href">A link to the Spotify Web API endpoint providing full details of the context.
        /// </param>
        /// <param name="externalUrls">
        /// A <see cref="IReadOnlyDictionary{TKey,TValue}"/> containing the known
        /// external URLs for the context, keyed by the type of the URL.
        /// </param>
        public Context(Uri uri, Uri href, IEnumerable<KeyValuePair<string, Uri>> externalUrls)
        {
            Uri = uri;
            Href = href;
            ExternalUrls = new ImmutableValueDictionary<string, Uri>(externalUrls);
        }

        /// <summary>
        /// Gets or sets the <see href="https://spotify.dev/documentation/web-api/#spotify-uris-and-ids">Spotify URI</see>
        /// of the <see cref="Context"/>.
        /// </summary>
        /// <returns>The Spotify URI of the <see cref="Context"/>.</returns>
        public Uri Uri { get; init; }

        /// <summary>
        /// Gets or sets a link to the Spotify Web API endpoint providing full details of the <see cref="Context"/>.
        /// </summary>
        /// <returns>A link to the Spotify Web API endpoint providing full details of the <see cref="Context"/>.</returns>
        public Uri Href { get; init; }

        /// <summary>
        /// Gets or sets the known external URLs for the <see cref="Context"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="IReadOnlyDictionary{TKey, TValue}"/> containing the known external URLs for the
        /// <see cref="Context"/>, keyed by the type of the URL.
        /// </returns>
        public IReadOnlyDictionary<string, Uri> ExternalUrls { get; init; }
    }
}
