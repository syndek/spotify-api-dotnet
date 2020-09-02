using System;
using System.Collections.Generic;

namespace Spotify.ObjectModel
{
    public record SimplifiedShow : LocatableObject
    {
        internal SimplifiedShow(
            String id,
            Uri uri,
            Uri href,
            String name,
            String description,
            IReadOnlyList<Image> images,
            Boolean isExplicit,
            Boolean? isExternallyHosted,
            IReadOnlyList<String> languages,
            IReadOnlyList<CountryCode> availableMarkets,
            String mediaType,
            String publisher,
            IReadOnlyList<Copyright> copyrights,
            IReadOnlyDictionary<String, Uri> externalUrls) :
            base(id, uri)
        {
            this.Href = href;
            this.Name = name;
            this.Description = description;
            this.Images = images;
            this.IsExplicit = isExplicit;
            this.IsExternallyHosted = isExternallyHosted;
            this.Languages = languages;
            this.AvailableMarkets = availableMarkets;
            this.MediaType = mediaType;
            this.Publisher = publisher;
            this.Copyrights = copyrights;
            this.ExternalUrls = externalUrls;
        }

        public Uri Href { get; init; }
        public String Name { get; init; }
        public String Description { get; init; }
        public IReadOnlyList<Image> Images { get; init; }
        public Boolean IsExplicit { get; init; }
        public Boolean? IsExternallyHosted { get; init; }
        public IReadOnlyList<String> Languages { get; init; }
        public IReadOnlyList<CountryCode> AvailableMarkets { get; init; }
        public String MediaType { get; init; }
        public String Publisher { get; init; }
        public IReadOnlyList<Copyright> Copyrights { get; init; }
        public IReadOnlyDictionary<String, Uri> ExternalUrls { get; init; }
    }
}