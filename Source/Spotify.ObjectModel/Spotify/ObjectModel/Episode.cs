using System;
using System.Collections.Generic;

namespace Spotify.ObjectModel
{
    public record Episode : SimplifiedEpisode, IPlayable
    {
        public Episode(
            string id,
            Uri uri,
            Uri href,
            string name,
            string description,
            IReadOnlyList<Image> images,
            SimplifiedShow show,
            int duration,
            DateTime releaseDate,
            ReleaseDatePrecision releaseDatePrecision,
            bool isExplicit,
            bool isPlayable,
            bool isExternallyHosted,
            IReadOnlyList<string> languages,
            Uri? audioPreviewUrl,
            IReadOnlyDictionary<string, Uri> externalUrls,
            ResumePoint? resumePoint) :
            base(
                id,
                uri,
                href,
                name,
                description,
                images,
                duration,
                releaseDate,
                releaseDatePrecision,
                isExplicit,
                isPlayable,
                isExternallyHosted,
                languages,
                audioPreviewUrl,
                externalUrls,
                resumePoint)
        {
            Show = show;
        }

        public SimplifiedShow Show { get; init; }
    }
}