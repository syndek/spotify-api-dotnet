using Spotify.ObjectModel.Collections;
using System;
using System.Collections.Generic;

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
            IEnumerable<Image> images,
            bool isExplicit,
            bool? isExternallyHosted,
            IEnumerable<string> languages,
            IEnumerable<CountryCode> availableMarkets,
            string mediaType,
            string publisher,
            IEnumerable<Copyright> copyrights,
            IEnumerable<KeyValuePair<string, Uri>> externalUrls)
            : base(id, uri)
        {
            Href = href;
            Name = name;
            Description = description;
            Images = new ImmutableValueArray<Image>(images);
            IsExplicit = isExplicit;
            IsExternallyHosted = isExternallyHosted;
            Languages = new ImmutableValueArray<string>(languages);
            AvailableMarkets = new ImmutableValueArray<CountryCode>(availableMarkets);
            MediaType = mediaType;
            Publisher = publisher;
            Copyrights = new ImmutableValueArray<Copyright>(copyrights);
            ExternalUrls = new ImmutableValueDictionary<string, Uri>(externalUrls);
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
