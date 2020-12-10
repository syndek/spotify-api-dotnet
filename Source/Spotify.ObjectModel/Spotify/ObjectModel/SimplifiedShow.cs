using System;
using System.Collections.Generic;

using Spotify.ObjectModel.Collections;

namespace Spotify.ObjectModel
{
    public record SimplifiedShow : LocatableObject
    {
        public SimplifiedShow(
            string id,
            Uri uri,
            Uri href,
            string name,
            string description,
            IReadOnlyList<Image> images,
            bool isExplicit,
            bool? isExternallyHosted,
            IReadOnlyList<string> languages,
            IReadOnlyList<CountryCode> availableMarkets,
            string mediaType,
            string publisher,
            IReadOnlyList<Copyright> copyrights,
            IReadOnlyDictionary<string, Uri> externalUrls) :
            base(id, uri)
        {
            this.Href = href;
            this.Name = name;
            this.Description = description;
            this.Images = new ImmutableValueArray<Image>(images);
            this.IsExplicit = isExplicit;
            this.IsExternallyHosted = isExternallyHosted;
            this.Languages = new ImmutableValueArray<string>(languages);
            this.AvailableMarkets = new ImmutableValueArray<CountryCode>(availableMarkets);
            this.MediaType = mediaType;
            this.Publisher = publisher;
            this.Copyrights = new ImmutableValueArray<Copyright>(copyrights);
            this.ExternalUrls = new ImmutableValueDictionary<string, Uri>(externalUrls);
        }

        public Uri Href { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public IReadOnlyList<Image> Images { get; init; }
        public bool IsExplicit { get; init; }
        public bool? IsExternallyHosted { get; init; }
        public IReadOnlyList<string> Languages { get; init; }
        public IReadOnlyList<CountryCode> AvailableMarkets { get; init; }
        public string MediaType { get; init; }
        public string Publisher { get; init; }
        public IReadOnlyList<Copyright> Copyrights { get; init; }
        public IReadOnlyDictionary<string, Uri> ExternalUrls { get; init; }
    }
}