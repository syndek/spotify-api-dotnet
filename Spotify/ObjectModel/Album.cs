using System;
using System.Collections.Generic;

namespace Spotify.ObjectModel
{
    public record Album : SimplifiedAlbum, ISaveable
    {
        internal Album(
            String id,
            Uri uri,
            Uri href,
            String name,
            AlbumType type,
            IReadOnlyList<Image> images,
            IReadOnlyList<SimplifiedArtist> artists,
            DateTime releaseDate,
            ReleaseDatePrecision releaseDatePrecision,
            Paging<SimplifiedTrack> tracks,
            IReadOnlyList<String> genres,
            Int32 popularity,
            IReadOnlyList<CountryCode> availableMarkets,
            String label,
            IReadOnlyList<Copyright> copyrights,
            IReadOnlyDictionary<String, String> externalIds,
            IReadOnlyDictionary<String, Uri> externalUrls) :
            base(
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
            this.Tracks = tracks;
            this.Genres = genres;
            this.Popularity = popularity;
            this.Label = label;
            this.Copyrights = copyrights;
            this.ExternalIds = externalIds;
        }

        public Paging<SimplifiedTrack> Tracks { get; init; }
        public IReadOnlyList<String> Genres { get; init; }
        public Int32 Popularity { get; init; }
        public String Label { get; init; }
        public IReadOnlyList<Copyright> Copyrights { get; init; }
        public IReadOnlyDictionary<String, String> ExternalIds { get; init; }
    }
}