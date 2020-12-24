using System;
using System.Collections.Generic;
using Spotify.ObjectModel.Collections;

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
            IEnumerable<Image> images,
            Paging<SimplifiedEpisode> episodes,
            bool isExplicit,
            bool? isExternallyHosted,
            IEnumerable<string> languages,
            IEnumerable<CountryCode> availableMarkets,
            string mediaType,
            string publisher,
            IEnumerable<Copyright> copyrights,
            IEnumerable<KeyValuePair<string, Uri>> externalUrls)
            : base(
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
