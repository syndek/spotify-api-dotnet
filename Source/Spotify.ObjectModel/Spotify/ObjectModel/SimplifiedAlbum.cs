using Spotify.ObjectModel.Collections;
using System;
using System.Collections.Generic;

namespace Spotify.ObjectModel
{
    /// <summary>
    /// Represents a simplified view of an <see cref="Album"/>.
    /// </summary>
    public record SimplifiedAlbum : LocatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SimplifiedAlbum"/> record with the specified values.
        /// </summary>
        /// <param name="id">A <see cref="string"/> representing the Spotify ID of the album.</param>
        /// <param name="uri">The Spotify URI of the album.</param>
        /// <param name="href">A link to the Spotify Web API endpoint providing full details of the album.</param>
        /// <param name="name">A <see cref="string"/> representing the name of the album.</param>
        /// <param name="type">The type of the album.</param>
        /// <param name="group">
        /// The relationship between an artist and the album, or <see langword="null"/> if not applicable in the current context.
        /// </param>
        /// <param name="images">
        /// A <see cref="IReadOnlyList{T}"/> of <see cref="Image"/> objects
        /// representing the cover art of the album in various sizes.
        /// </param>
        /// <param name="artists">
        /// A <see cref="IReadOnlyList{T}"/> of <see cref="SimplifiedArtist"/> objects representing the artists of the album.
        /// </param>
        /// <param name="releaseDate">The release date of the album.</param>
        /// <param name="releaseDatePrecision">The precision with which the <paramref name="releaseDate"/> value is known.</param>
        /// <param name="availableMarkets">
        /// A <see cref="IReadOnlyList{T}"/> of <see cref="CountryCode"/> values representing the markets in which the album is available.
        /// </param>
        /// <param name="externalUrls">
        /// A <see cref="IReadOnlyDictionary{TKey, TValue}"/> containing the known
        /// external URLs for the album, keyed by the type of the URL.
        /// </param>
        public SimplifiedAlbum(
            string id,
            Uri uri,
            Uri href,
            string name,
            AlbumType type,
            AlbumGroups? group,
            IReadOnlyList<Image> images,
            IReadOnlyList<SimplifiedArtist> artists,
            DateTime releaseDate,
            ReleaseDatePrecision releaseDatePrecision,
            IReadOnlyList<CountryCode> availableMarkets,
            IReadOnlyDictionary<string, Uri> externalUrls) :
            base(id, uri)
        {
            Href = href;
            Name = name;
            Type = type;
            Group = group;
            Images = new ImmutableValueArray<Image>(images);
            Artists = new ImmutableValueArray<SimplifiedArtist>(artists);
            ReleaseDate = releaseDate;
            ReleaseDatePrecision = releaseDatePrecision;
            AvailableMarkets = new ImmutableValueArray<CountryCode>(availableMarkets);
            ExternalUrls = new ImmutableValueDictionary<string, Uri>(externalUrls);
        }

        /// <summary>
        /// Gets or sets a link to the Spotify Web API endpoint providing full details of the <see cref="SimplifiedAlbum"/>.
        /// </summary>
        /// <returns>A link to the Spotify Web API endpoint providing full details of the <see cref="SimplifiedAlbum"/>.</returns>
        public Uri Href { get; init; }
        /// <summary>
        /// Gets or sets the name of the <see cref="SimplifiedAlbum"/>.
        /// </summary>
        /// <returns>A <see cref="string"/> representing the name of the <see cref="SimplifiedAlbum"/>.</returns>
        public string Name { get; init; }
        /// <summary>
        /// Gets or sets the type of the <see cref="SimplifiedAlbum"/>.
        /// </summary>
        /// <returns>The type of the <see cref="SimplifiedAlbum"/>.</returns>
        public AlbumType Type { get; init; }
        /// <summary>
        /// Gets or sets the relationship between an artist and the <see cref="SimplifiedAlbum"/>.
        /// </summary>
        /// <returns>
        /// The relationship between an artist and the <see cref="SimplifiedAlbum"/>,
        /// or <see langword="null"/> if not applicable in the current context.
        /// </returns>
        public AlbumGroups? Group { get; init; }
        /// <summary>
        /// Gets or sets the cover art of the <see cref="SimplifiedAlbum"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="IReadOnlyList{T}"/> of <see cref="Image"/> objects
        /// representing the cover art of the <see cref="SimplifiedAlbum"/> in various sizes.
        /// </returns>
        public IReadOnlyList<Image> Images { get; init; }
        /// <summary>
        /// Gets or sets the artists of the <see cref="SimplifiedAlbum"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="IReadOnlyList{T}"/> of <see cref="SimplifiedArtist"/> objects
        /// representing the artists of the <see cref="SimplifiedAlbum"/>.
        /// </returns>
        public IReadOnlyList<SimplifiedArtist> Artists { get; init; }
        /// <summary>
        /// Gets or sets the release date of the <see cref="SimplifiedAlbum"/>.
        /// </summary>
        /// <returns>The release date of the <see cref="SimplifiedAlbum"/>.</returns>
        public DateTime ReleaseDate { get; init; }
        /// <summary>
        /// Gets or sets the precision with which the <see cref="ReleaseDate"/> value is known.
        /// </summary>
        /// <returns>The precision with which the <see cref="ReleaseDate"/> value is known.</returns>
        public ReleaseDatePrecision ReleaseDatePrecision { get; init; }
        /// <summary>
        /// Gets or sets the markets in which the <see cref="SimplifiedAlbum"/> is available.
        /// </summary>
        /// <returns>
        /// A <see cref="IReadOnlyList{T}"/> of <see cref="CountryCode"/> values representing
        /// the markets in which the <see cref="SimplifiedAlbum"/> is available.
        /// </returns>
        public IReadOnlyList<CountryCode> AvailableMarkets { get; init; }
        /// <summary>
        /// Gets or sets the known external URLs for the <see cref="SimplifiedAlbum"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="IReadOnlyDictionary{TKey, TValue}"/> containing the known
        /// external URLs for the <see cref="SimplifiedAlbum"/>, keyed by the type of the URL.
        /// </returns>
        public IReadOnlyDictionary<string, Uri> ExternalUrls { get; init; }
    }
}