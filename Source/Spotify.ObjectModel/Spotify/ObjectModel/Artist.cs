using System;
using System.Collections.Generic;

using Spotify.ObjectModel.Collections;

namespace Spotify.ObjectModel
{
    /// <summary>
    /// Represents the artist of an <see cref="Album"/> or <see cref="Track"/>.
    /// </summary>
    public record Artist : SimplifiedArtist
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Artist"/> record with the specified values.
        /// </summary>
        /// <param name="id">A <see cref="string"/> representing the Spotify ID of the artist.</param>
        /// <param name="uri">The Spotify URI of the artist.</param>
        /// <param name="href">A link to the Spotify Web API endpoint providing full details of the artist.</param>
        /// <param name="name">A <see cref="string"/> representing the name of the artist.</param>
        /// <param name="images">
        /// A <see cref="IReadOnlyList{T}"/> of <see cref="Image"/> objects
        /// representing images of the artist in various sizes.
        /// </param>
        /// <param name="followers">The <see cref="ObjectModel.Followers"/> of the artist.</param>
        /// <param name="genres">
        /// A <see cref="IReadOnlyList{T}"/> of <see cref="string"/> objects representing genres the artist is associated with.
        /// </param>
        /// <param name="popularity">An <see cref="int"/> representing the popularity of the artist.</param>
        /// <param name="externalUrls">
        /// A <see cref="IReadOnlyDictionary{TKey, TValue}"/> containing the known
        /// external URLs for the artist, keyed by the type of the URL.
        /// </param>
        public Artist(
            string id,
            Uri uri,
            Uri href,
            string name,
            IReadOnlyList<Image> images,
            Followers followers,
            IReadOnlyList<string> genres,
            int popularity,
            IReadOnlyDictionary<string, Uri> externalUrls) :
            base(id, uri, href, name, externalUrls)
        {
            this.Images = new ImmutableValueArray<Image>(images);
            this.Followers = followers;
            this.Genres = new ImmutableValueArray<string>(genres);
            this.Popularity = popularity;
        }

        /// <summary>
        /// Gets or sets the images of the <see cref="Artist"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="IReadOnlyList{T}"/> of <see cref="Image"/> objects
        /// representing images of the <see cref="Artist"/> in various sizes.
        /// </returns>
        public IReadOnlyList<Image> Images { get; init; }
        /// <summary>
        /// Gets or sets the <see cref="ObjectModel.Followers"/> of the <see cref="Artist"/>.
        /// </summary>
        /// <returns>The <see cref="ObjectModel.Followers"/> of the <see cref="Artist"/>.</returns>
        public Followers Followers { get; init; }
        /// <summary>
        /// Gets or sets the genres associated with the <see cref="Artist"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="IReadOnlyList{T}"/> of <see cref="string"/> objects representing genres the <see cref="Artist"/> is associated with.
        /// </returns>
        public IReadOnlyList<string> Genres { get; init; }
        /// <summary>
        /// Gets or sets the popularity of the <see cref="Artist"/>.
        /// Popularity values will be between <c>0</c> and <c>100</c>, with <c>100</c> being the most popular.
        /// </summary>
        /// <returns>An <see cref="int"/> representing the popularity of the <see cref="Artist"/>.</returns>
        public int Popularity { get; init; }
    }
}