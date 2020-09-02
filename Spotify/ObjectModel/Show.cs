using System;
using System.Collections.Generic;

namespace Spotify.ObjectModel
{
    public record Show : SimplifiedShow, ISaveable
    {
        internal Show(
            String id,
            Uri uri,
            Uri href,
            String name,
            String description,
            IReadOnlyList<Image> images,
            IReadOnlyList<SimplifiedEpisode> episodes,
            Boolean isExplicit,
            Boolean? isExternallyHosted,
            IReadOnlyList<String> languages,
            IReadOnlyList<CountryCode> availableMarkets,
            String mediaType,
            String publisher,
            IReadOnlyList<Copyright> copyrights,
            IReadOnlyDictionary<String, Uri> externalUrls) :
            base(
                id,
                uri,
                href,
                name,
                description,
                images,
                isExplicit,
                isExternallyHosted,
                languages,
                availableMarkets,
                mediaType,
                publisher,
                copyrights,
                externalUrls)
        {
            this.Episodes = episodes;
        }

        public IReadOnlyList<SimplifiedEpisode> Episodes { get; init; }
    }
}