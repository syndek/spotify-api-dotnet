using System;
using System.Collections.Generic;

namespace Spotify.ObjectModel
{
    public record Episode : SimplifiedEpisode, IPlayable
    {
        public Episode(
            String id,
            Uri uri,
            Uri href,
            String name,
            String description,
            IReadOnlyList<Image> images,
            SimplifiedShow show,
            Int32 duration,
            DateTime releaseDate,
            ReleaseDatePrecision releaseDatePrecision,
            Boolean isExplicit,
            Boolean isPlayable,
            Boolean isExternallyHosted,
            IReadOnlyList<String> languages,
            Uri? audioPreviewUrl,
            IReadOnlyDictionary<String, Uri> externalUrls,
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
            this.Show = show;
        }

        public SimplifiedShow Show { get; init; }
    }
}