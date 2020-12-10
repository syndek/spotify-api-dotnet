using Spotify.ObjectModel.Collections;
using System;
using System.Collections.Generic;

namespace Spotify.ObjectModel
{
    public record Show : SimplifiedShow, ISaveable
    {
        public Show(
            string id,
            Uri uri,
            Uri href,
            string name,
            string description,
            IReadOnlyList<Image> images,
            Paging<SimplifiedEpisode> episodes,
            bool isExplicit,
            bool? isExternallyHosted,
            IReadOnlyList<string> languages,
            IReadOnlyList<CountryCode> availableMarkets,
            string mediaType,
            string publisher,
            IReadOnlyList<Copyright> copyrights,
            IReadOnlyDictionary<string, Uri> externalUrls) :
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
            Episodes = episodes;
        }

        public Paging<SimplifiedEpisode> Episodes { get; init; }
    }
}