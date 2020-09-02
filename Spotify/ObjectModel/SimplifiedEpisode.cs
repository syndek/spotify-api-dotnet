using System;
using System.Collections.Generic;

namespace Spotify.ObjectModel
{
    public record SimplifiedEpisode : LocatableObject
    {
        internal SimplifiedEpisode(
            String id,
            Uri uri,
            Uri href,
            String name,
            String description,
            IReadOnlyList<Image> images,
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
            base(id, uri)
        {
            this.Href = href;
            this.Name = name;
            this.Description = description;
            this.Images = images;
            this.Duration = duration;
            this.ReleaseDate = releaseDate;
            this.ReleaseDatePrecision = releaseDatePrecision;
            this.IsExplicit = isExplicit;
            this.IsPlayable = isPlayable;
            this.IsExternallyHosted = isExternallyHosted;
            this.Languages = languages;
            this.AudioPreviewUrl = audioPreviewUrl;
            this.ExternalUrls = externalUrls;
            this.ResumePoint = resumePoint;
        }

        public Uri Href { get; init; }
        public String Name { get; init; }
        public String Description { get; init; }
        public IReadOnlyList<Image> Images { get; init; }
        public Int32 Duration { get; init; }
        public DateTime ReleaseDate { get; init; }
        public ReleaseDatePrecision ReleaseDatePrecision { get; init; }
        public Boolean IsExplicit { get; init; }
        public Boolean IsPlayable { get; init; }
        public Boolean IsExternallyHosted { get; init; }
        public IReadOnlyList<String> Languages { get; init; }
        public Uri? AudioPreviewUrl { get; init; }
        public IReadOnlyDictionary<String, Uri> ExternalUrls { get; init; }
        public ResumePoint? ResumePoint { get; init; }
    }
}