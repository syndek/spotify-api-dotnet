using System;
using System.Collections.Generic;

using Spotify.ObjectModel.Collections;

namespace Spotify.ObjectModel
{
    public record Show : SimplifiedShow, ISaveable
    {
        public Show(
            String id,
            Uri uri,
            Uri href,
            String name,
            String description,
            IReadOnlyList<Image> images,
            Paging<SimplifiedEpisode> episodes,
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

        public Paging<SimplifiedEpisode> Episodes { get; init; }
    }
}