using Spotify.ObjectModel.Collections;
using System;
using System.Collections.Generic;

namespace Spotify.ObjectModel
{
    public record SimplifiedEpisode : LocatableObject
    {
        public SimplifiedEpisode(
            string id,
            Uri uri,
            Uri href,
            string name,
            string description,
            IReadOnlyList<Image> images,
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
            this.Languages = new ImmutableValueArray<string>(languages);
            this.AudioPreviewUrl = audioPreviewUrl;
            this.ExternalUrls = new ImmutableValueDictionary<string, Uri>(externalUrls);
            this.ResumePoint = resumePoint;
        }

        public Uri Href { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public IReadOnlyList<Image> Images { get; init; }
        public int Duration { get; init; }
        public DateTime ReleaseDate { get; init; }
        public ReleaseDatePrecision ReleaseDatePrecision { get; init; }
        public bool IsExplicit { get; init; }
        public bool IsPlayable { get; init; }
        public bool IsExternallyHosted { get; init; }
        public IReadOnlyList<string> Languages { get; init; }
        public Uri? AudioPreviewUrl { get; init; }
        public IReadOnlyDictionary<string, Uri> ExternalUrls { get; init; }
        public ResumePoint? ResumePoint { get; init; }
    }
}