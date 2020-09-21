using System;
using System.Collections.Generic;

namespace Spotify.ObjectModel
{
    public record SimplifiedTrack : LocatableObject
    {
        public SimplifiedTrack(
            String id,
            Uri uri,
            Uri href,
            String name,
            IReadOnlyList<SimplifiedArtist> artists,
            Int32 duration,
            Int32 discNumber,
            Int32 trackNumber,
            Boolean isExplicit,
            Boolean isLocal,
            IReadOnlyList<CountryCode> availableMarkets,
            String previewUrl,
            IReadOnlyDictionary<String, Uri> externalUrls) :
            base(id, uri)
        {
            this.Href = href;
            this.Name = name;
            this.Artists = artists;
            this.Duration = duration;
            this.DiscNumber = discNumber;
            this.TrackNumber = trackNumber;
            this.IsExplicit = isExplicit;
            this.IsLocal = isLocal;
            this.AvailableMarkets = availableMarkets;
            this.PreviewUrl = previewUrl;
            this.ExternalUrls = externalUrls;
        }

        public Uri Href { get; init; }
        public String Name { get; init; }
        public IReadOnlyList<SimplifiedArtist> Artists { get; init; }
        public Int32 Duration { get; init; }
        public Int32 DiscNumber { get; init; }
        public Int32 TrackNumber { get; init; }
        public Boolean IsExplicit { get; init; }
        public Boolean IsLocal { get; init; }
        public IReadOnlyList<CountryCode> AvailableMarkets { get; init; }
        public String PreviewUrl { get; init; }
        public IReadOnlyDictionary<String, Uri> ExternalUrls { get; init; }
    }
}