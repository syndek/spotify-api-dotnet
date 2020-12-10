using Spotify.ObjectModel.Collections;
using System;
using System.Collections.Generic;

namespace Spotify.ObjectModel
{
    public record Track : SimplifiedTrack, IPlayable, ISaveable
    {
        public Track(
            string id,
            Uri uri,
            Uri href,
            string name,
            SimplifiedAlbum album,
            IReadOnlyList<SimplifiedArtist> artists,
            int duration,
            int discNumber,
            int trackNumber,
            bool isExplicit,
            bool isLocal,
            IReadOnlyList<CountryCode> availableMarkets,
            int popularity,
            string previewUrl,
            IReadOnlyDictionary<string, string> externalIds,
            IReadOnlyDictionary<string, Uri> externalUrls)
            : base(
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
            Album = album;
            Popularity = popularity;
            ExternalIds = new ImmutableValueDictionary<string, string>(externalIds);
        }

        public SimplifiedAlbum Album { get; init; }
        public int Popularity { get; init; }
        public IReadOnlyDictionary<string, string> ExternalIds { get; init; }
    }
}