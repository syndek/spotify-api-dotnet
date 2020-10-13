using System;
using System.Collections.Generic;

using Spotify.ObjectModel.Collections;

namespace Spotify.ObjectModel
{
    public record SimplifiedEpisode : LocatableObject
    {
        public SimplifiedEpisode(
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
            this.Images = new ImmutableValueArray<Image>(images);
            this.Duration = duration;
            this.ReleaseDate = releaseDate;
            this.ReleaseDatePrecision = releaseDatePrecision;
            this.IsExplicit = isExplicit;
            this.IsPlayable = isPlayable;
            this.IsExternallyHosted = isExternallyHosted;
            this.Languages = new ImmutableValueArray<String>(languages);
            this.AudioPreviewUrl = audioPreviewUrl;
            this.ExternalUrls = new ImmutableValueDictionary<String, Uri>(externalUrls);
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