using System;
using System.Collections.Generic;

namespace Spotify.ObjectModel
{
    public record SimplifiedAlbum : LocatableObject
    {
        internal SimplifiedAlbum(
            String id,
            Uri uri,
            Uri href,
            String name,
            AlbumType type,
            AlbumGroups? group,
            IReadOnlyList<Image> images,
            IReadOnlyList<SimplifiedArtist> artists,
            DateTime releaseDate,
            ReleaseDatePrecision releaseDatePrecision,
            IReadOnlyList<CountryCode> availableMarkets,
            IReadOnlyDictionary<String, Uri> externalUrls) :
            base(id, uri)
        {
            this.Href = href;
            this.Name = name;
            this.Type = type;
            this.Group = group;
            this.Images = images;
            this.Artists = artists;
            this.ReleaseDate = releaseDate;
            this.ReleaseDatePrecision = releaseDatePrecision;
            this.AvailableMarkets = availableMarkets;
            this.ExternalUrls = externalUrls;
        }

        public Uri Href { get; init; }
        public String Name { get; init; }
        public AlbumType Type { get; init; }
        public AlbumGroups? Group { get; init; }
        public IReadOnlyList<Image> Images { get; init; }
        public IReadOnlyList<SimplifiedArtist> Artists { get; init; }
        public DateTime ReleaseDate { get; init; }
        public ReleaseDatePrecision ReleaseDatePrecision { get; init; }
        public IReadOnlyList<CountryCode> AvailableMarkets { get; init; }
        public IReadOnlyDictionary<String, Uri> ExternalUrls { get; init; }
    }
}