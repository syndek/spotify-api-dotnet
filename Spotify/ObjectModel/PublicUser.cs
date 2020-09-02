using System;
using System.Collections.Generic;

namespace Spotify.ObjectModel
{
    public record PublicUser : LocatableObject
    {
        internal PublicUser(
            String id,
            Uri uri,
            Uri href,
            String? displayName,
            IReadOnlyList<Image> images,
            Followers followers,
            IReadOnlyDictionary<String, Uri> externalUrls)
            : base(id, uri)
        {
            this.Href = href;
            this.DisplayName = displayName;
            this.Images = images;
            this.Followers = followers;
            this.ExternalUrls = externalUrls;
        }

        public Uri Href { get; init; }
        public String? DisplayName { get; init; }
        public IReadOnlyList<Image> Images { get; init; }
        public Followers Followers { get; init; }
        public IReadOnlyDictionary<String, Uri> ExternalUrls { get; init; }
    }
}