using Spotify.ObjectModel.Collections;
using System;
using System.Collections.Generic;

namespace Spotify.ObjectModel
{
    public record SimplifiedTrack : LocatableObject
    {
        public SimplifiedTrack(
            string id,
            Uri uri,
            Uri href,
            string name,
            IReadOnlyList<SimplifiedArtist> artists,
            int duration,
            int discNumber,
            int trackNumber,
            bool isExplicit,
            bool isLocal,
            IReadOnlyList<CountryCode> availableMarkets,
            string previewUrl,
            IReadOnlyDictionary<string, Uri> externalUrls) :
            base(id, uri)
        {
            this.Href = href;
            this.Name = name;
            this.Artists = new ImmutableValueArray<SimplifiedArtist>(artists);
            this.Duration = duration;
            this.DiscNumber = discNumber;
            this.TrackNumber = trackNumber;
            this.IsExplicit = isExplicit;
            this.IsLocal = isLocal;
            this.AvailableMarkets = new ImmutableValueArray<CountryCode>(availableMarkets);
            this.PreviewUrl = previewUrl;
            this.ExternalUrls = new ImmutableValueDictionary<string, Uri>(externalUrls);
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