using System;
using System.Collections.Generic;

namespace Spotify.ObjectModel
{
    /// <summary>
    /// Represents the artist of an <see cref="Album"/> or <see cref="Track"/>.
    /// </summary>
    public record Artist : SimplifiedArtist
    {
        internal Artist(
            String id,
            Uri uri,
            Uri href,
            String name,
            IReadOnlyList<Image> images,
            Followers followers,
            IReadOnlyList<String> genres,
            Int32 popularity,
            IReadOnlyDictionary<String, Uri> externalUrls) :
            base(id, uri, href, name, externalUrls)
        {
            this.Images = images;
            this.Followers = followers;
            this.Genres = genres;
            this.Popularity = popularity;
        }

        public IReadOnlyList<Image> Images { get; init; }
        public Followers Followers { get; init; }
        public IReadOnlyList<String> Genres { get; init; }
        public Int32 Popularity { get; init; }
    }
}