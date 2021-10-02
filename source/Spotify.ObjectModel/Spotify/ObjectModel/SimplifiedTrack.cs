using System;
using System.Collections.Generic;

using Spotify.ObjectModel.Collections;

namespace Spotify.ObjectModel
{
    public record SimplifiedTrack : LocatableObject
    {
        public SimplifiedTrack(
            string id,
            Uri uri,
            Uri href,
            string name,
            IEnumerable<SimplifiedArtist> artists,
            int duration,
            int discNumber,
            int trackNumber,
            bool isExplicit,
            bool isLocal,
            IEnumerable<CountryCode> availableMarkets,
            string previewUrl,
            IEnumerable<KeyValuePair<string, Uri>> externalUrls)
            : base(id, uri)
        {
            Href = href;
            Name = name;
            Artists = new ImmutableValueArray<SimplifiedArtist>(artists);
            Duration = duration;
            DiscNumber = discNumber;
            TrackNumber = trackNumber;
            IsExplicit = isExplicit;
            IsLocal = isLocal;
            AvailableMarkets = new ImmutableValueArray<CountryCode>(availableMarkets);
            PreviewUrl = previewUrl;
            ExternalUrls = new ImmutableValueDictionary<string, Uri>(externalUrls);
        }

        public Uri Href { get; init; }
        public string Name { get; init; }
        public IReadOnlyList<SimplifiedArtist> Artists { get; init; }
        public int Duration { get; init; }
        public int DiscNumber { get; init; }
        public int TrackNumber { get; init; }
        public bool IsExplicit { get; init; }
        public bool IsLocal { get; init; }
        public IReadOnlyList<CountryCode> AvailableMarkets { get; init; }
        public string PreviewUrl { get; init; }
        public IReadOnlyDictionary<string, Uri> ExternalUrls { get; init; }
    }
}
