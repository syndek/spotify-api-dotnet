using System;
using System.Collections.Generic;

namespace Spotify.ObjectModel
{
    public record Track : SimplifiedTrack, IPlayable, ISaveable
    {
        public Track(
            String id,
            Uri uri,
            Uri href,
            String name,
            SimplifiedAlbum album,
            IReadOnlyList<SimplifiedArtist> artists,
            Int32 duration,
            Int32 discNumber,
            Int32 trackNumber,
            Boolean isExplicit,
            Boolean isLocal,
            IReadOnlyList<CountryCode> availableMarkets,
            Int32 popularity,
            String previewUrl,
            IReadOnlyDictionary<String, String> externalIds,
            IReadOnlyDictionary<String, Uri> externalUrls) :
            base(
                id,
                uri,
                href,
                name,
                artists,
                duration,
                discNumber,
                trackNumber,
                isExplicit,
                isLocal,
                availableMarkets,
                previewUrl,
                externalUrls)
        {
            this.Album = album;
            this.Popularity = popularity;
            this.ExternalIds = externalIds;
        }

        public SimplifiedAlbum Album { get; init; }
        public Int32 Popularity { get; init; }
        public IReadOnlyDictionary<String, String> ExternalIds { get; init; }
    }
}