using System;
using System.Collections.Generic;

using Spotify.ObjectModel.Collections;

namespace Spotify.ObjectModel
{
    /// <summary>
    /// Represents an album.
    /// </summary>
    public record Album : SimplifiedAlbum, ISaveable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Album"/> record with the specified values.
        /// </summary>
        /// <param name="id">A <see cref="string"/> representing the Spotify ID of the album.</param>
        /// <param name="uri">The Spotify URI of the album.</param>
        /// <param name="href">A link to the Spotify Web API endpoint providing full details of the album.</param>
        /// <param name="name">A <see cref="string"/> representing the name of the album.</param>
        /// <param name="type">The type of the album.</param>
        /// <param name="images">
        /// A <see cref="IReadOnlyList{T}"/> of <see cref="Image"/> objects
        /// representing the cover art of the album in various sizes.
        /// </param>
        /// <param name="artists">
        /// A <see cref="IReadOnlyList{T}"/> of <see cref="SimplifiedArtist"/> objects representing the artists of the album.
        /// </param>
        /// <param name="releaseDate">The release date of the album.</param>
        /// <param name="releaseDatePrecision">The precision with which the <paramref name="releaseDate"/> value is known.</param>
        /// <param name="tracks">
        /// A <see cref="Paging{TItem}"/> of <see cref="SimplifiedTrack"/> objects representing the tracks of the album.
        /// </param>
        /// <param name="genres">
        /// A <see cref="IReadOnlyList{T}"/> of <see cref="string"/> objects representing genres the album is associated with.
        /// </param>
        /// <param name="popularity">An <see cref="int"/> representing the popularity of the album.</param>
        /// <param name="availableMarkets">
        /// A <see cref="IReadOnlyList{T}"/> of <see cref="CountryCode"/> values representing the markets in which the album is available.
        /// </param>
        /// <param name="label">A <see cref="string"/> representing the name of the label that released the album.</param>
        /// <param name="copyrights">
        /// A <see cref="IReadOnlyList{T}"/> of <see cref="Copyright"/> objects representing the copyright statements of the album.
        /// </param>
        /// <param name="externalIds">
        /// A <see cref="IReadOnlyDictionary{TKey, TValue}"/> containing the known
        /// external IDs for the album, keyed by the type of ID.
        /// </param>
        /// <param name="externalUrls">
        /// A <see cref="IReadOnlyDictionary{TKey, TValue}"/> containing the known
        /// external URLs for the album, keyed by the type of the URL.
        /// </param>
        public Album(
            string id,
            Uri uri,
            Uri href,
            string name,
            AlbumType type,
            IEnumerable<Image> images,
            IEnumerable<SimplifiedArtist> artists,
            DateTime releaseDate,
            ReleaseDatePrecision releaseDatePrecision,
            Paging<SimplifiedTrack> tracks,
            IEnumerable<string> genres,
            int popularity,
            IEnumerable<CountryCode> availableMarkets,
            string label,
            IEnumerable<Copyright> copyrights,
            IEnumerable<KeyValuePair<string, string>> externalIds,
            IEnumerable<KeyValuePair<string, Uri>> externalUrls)
            : base(
                id,
                uri,
                href,
                name,
                type,
                null,
                images,
                artists,
                releaseDate,
                releaseDatePrecision,
                availableMarkets,
                externalUrls)
        {
            Tracks = tracks;
            Genres = new ImmutableValueArray<string>(genres);
            Popularity = popularity;
            Label = label;
            Copyrights = new ImmutableValueArray<Copyright>(copyrights);
            ExternalIds = new ImmutableValueDictionary<string, string>(externalIds);
        }

        /// <summary>
        /// Gets or sets the tracks of the <see cref="Album"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="Paging{TItem}"/> of <see cref="SimplifiedTrack"/> objects
        /// representing the tracks of the <see cref="Album"/>.
        /// </returns>
        public Paging<SimplifiedTrack> Tracks { get; init; }

        /// <summary>
        /// Gets or sets the genres associated with the <see cref="Album"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="IReadOnlyList{T}"/> of <see cref="string"/> objects representing genres the <see cref="Artist"/> is associated with.
        /// </returns>
        public IReadOnlyList<string> Genres { get; init; }

        /// <summary>
        /// Gets or sets the popularity of the <see cref="Album"/>.
        /// </summary>
        /// <returns>An <see cref="int"/> representing the popularity of the <see cref="Album"/>.</returns>
        public int Popularity { get; init; }

        /// <summary>
        /// Gets or sets the name of the label that released the <see cref="Album"/>.
        /// </summary>
        /// <returns>A <see cref="string"/> representing the name of the label that released the <see cref="Album"/>.</returns>
        public string Label { get; init; }

        /// <summary>
        /// Gets or sets the copyright statements of the <see cref="Album"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="IReadOnlyList{T}"/> of <see cref="Copyright"/> objects
        /// representing the copyright statements of the <see cref="Album"/>.
        /// </returns>
        public IReadOnlyList<Copyright> Copyrights { get; init; }

        /// <summary>
        /// Gets or sets the known external IDs for the <see cref="Album"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="IReadOnlyDictionary{TKey, TValue}"/> containing the known
        /// external IDs for the <see cref="Album"/>, keyed by the type of the ID.
        /// </returns>
        public IReadOnlyDictionary<string, string> ExternalIds { get; init; }
    }
}
