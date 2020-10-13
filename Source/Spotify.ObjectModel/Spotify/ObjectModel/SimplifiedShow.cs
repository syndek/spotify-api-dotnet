using System;
using System.Collections.Generic;

using Spotify.ObjectModel.Collections;

namespace Spotify.ObjectModel
{
    public record SimplifiedShow : LocatableObject
    {
        public SimplifiedShow(
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
            this.Images = new ImmutableValueArray<Image>(images);
            this.IsExplicit = isExplicit;
            this.IsExternallyHosted = isExternallyHosted;
            this.Languages = new ImmutableValueArray<String>(languages);
            this.AvailableMarkets = new ImmutableValueArray<CountryCode>(availableMarkets);
            this.MediaType = mediaType;
            this.Publisher = publisher;
            this.Copyrights = new ImmutableValueArray<Copyright>(copyrights);
            this.ExternalUrls = new ImmutableValueDictionary<String, Uri>(externalUrls);
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