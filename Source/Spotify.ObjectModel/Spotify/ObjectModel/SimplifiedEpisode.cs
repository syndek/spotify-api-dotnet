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
            IEnumerable<Image> images,
            int duration,
            DateTime releaseDate,
            ReleaseDatePrecision releaseDatePrecision,
            bool isExplicit,
            bool isPlayable,
            bool isExternallyHosted,
            IEnumerable<string> languages,
            Uri? audioPreviewUrl,
            IEnumerable<KeyValuePair<string, Uri>> externalUrls,
            ResumePoint? resumePoint)
            : base(id, uri)
        {
            Href = href;
            Name = name;
            Description = description;
            Images = new ImmutableValueArray<Image>(images);
            Duration = duration;
            ReleaseDate = releaseDate;
            ReleaseDatePrecision = releaseDatePrecision;
            IsExplicit = isExplicit;
            IsPlayable = isPlayable;
            IsExternallyHosted = isExternallyHosted;
            Languages = new ImmutableValueArray<string>(languages);
            AudioPreviewUrl = audioPreviewUrl;
            ExternalUrls = new ImmutableValueDictionary<string, Uri>(externalUrls);
            ResumePoint = resumePoint;
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
