using System;
using System.Collections.Generic;

namespace Spotify.ObjectModel
{
    public record PrivateUser : PublicUser
    {
        internal PrivateUser(
            String id,
            Uri uri,
            Uri href,
            String? email,
            String? displayName,
            CountryCode? country,
            IReadOnlyList<Image> images,
            Product? product,
            Followers followers,
            IReadOnlyDictionary<String, Uri> externalUrls) :
            base(id, uri, href, displayName, images, followers, externalUrls)
        {
            this.Email = email;
            this.Country = country;
            this.Product = product;
        }

        public String? Email { get; init; }
        public CountryCode? Country { get; init; }
        public Product? Product { get; init; }
    }
}