using System;
using System.Collections.Generic;

namespace Spotify.ObjectModel
{
    /// <summary>
    /// Represents a Spotify user's private profile.
    /// </summary>
    public record PrivateUser : PublicUser
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PrivateUser"/> record with the specified values.
        /// </summary>
        /// <param name="id">A <see cref="string"/> representing the Spotify ID of the user.</param>
        /// <param name="uri">The Spotify URI for the user.</param>
        /// <param name="href">A link to the Spotify Web API endpoint providing full details of the user.</param>
        /// <param name="email">
        /// A <see cref="string"/> representing the email address of the user,
        /// or <see langword="null"/> if not provided.
        /// </param>
        /// <param name="displayName">
        /// A <see cref="string"/> representing the display name of the user,
        /// or <see langword="null"/> if not available.
        /// </param>
        /// <param name="country">
        /// A <see cref="CountryCode"/> representing the country of the user, as set in their profile,
        /// or <see langword="null"/> if not provided.
        /// </param>
        /// <param name="images">
        /// A <see cref="IReadOnlyList{T}"/> of <see cref="Image"/> objects
        /// representing the profile image of the user in various sizes.
        /// </param>
        /// <param name="product">
        /// The Spotify <see cref="ObjectModel.Product"/> of the user,
        /// or <see langword="null"/> if not provided.
        /// </param>
        /// <param name="followers">The <see cref="Followers"/> of the user.</param>
        /// <param name="externalUrls">
        /// A <see cref="IReadOnlyDictionary{TKey, TValue}"/> containing the known
        /// external URLs for the user, keyed by the type of the URL.
        /// </param>
        public PrivateUser(
            string id,
            Uri uri,
            Uri href,
            string? email,
            string? displayName,
            CountryCode? country,
            IReadOnlyList<Image> images,
            Product? product,
            Followers followers,
            IReadOnlyDictionary<string, Uri> externalUrls) :
            base(id, uri, href, displayName, images, followers, externalUrls)
        {
            Email = email;
            Country = country;
            Product = product;
        }

        /// <summary>
        /// Gets or sets the email address of the <see cref="PrivateUser"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="string"/> representing the email address of the <see cref="PrivateUser"/>,
        /// or <see langword="null"/> if not provided.
        /// </returns>
        public string? Email { get; init; }
        /// <summary>
        /// Gets or sets the country of the <see cref="PrivateUser"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="CountryCode"/> representing the country of the <see cref="PrivateUser"/>, as set in their profile,
        /// or <see langword="null"/> if not provided.
        /// </returns>
        public CountryCode? Country { get; init; }
        /// <summary>
        /// Gets or sets the <see cref="ObjectModel.Product"/> of the <see cref="PrivateUser"/>.
        /// </summary>
        /// <returns>
        /// The Spotify <see cref="ObjectModel.Product"/> of the <see cref="PrivateUser"/>,
        /// or <see langword="null"/> if not provided.
        /// </returns>
        public Product? Product { get; init; }
    }
}