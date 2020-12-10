using Spotify.ObjectModel.Collections;
using System;
using System.Collections.Generic;

namespace Spotify.ObjectModel
{
    /// <summary>
    /// Represents a Spotify user's public profile.
    /// </summary>
    public record PublicUser : LocatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PublicUser"/> record with the specified values.
        /// </summary>
        /// <param name="id">A <see cref="string"/> representing the Spotify ID of the user.</param>
        /// <param name="uri">The Spotify URI for the user.</param>
        /// <param name="href">A link to the Spotify Web API endpoint providing full details of the user.</param>
        /// <param name="displayName">
        /// A <see cref="string"/> representing the display name of the user,
        /// or <see langword="null"/> if not available.
        /// </param>
        /// <param name="images">
        /// A <see cref="IReadOnlyList{T}"/> of <see cref="Image"/> objects
        /// representing the profile image of the user in various sizes.
        /// </param>
        /// <param name="followers">The <see cref="ObjectModel.Followers"/> of the user.</param>
        /// <param name="externalUrls">
        /// A <see cref="IReadOnlyDictionary{TKey, TValue}"/> containing the known
        /// external URLs for the user, keyed by the type of the URL.
        /// </param>
        public PublicUser(
            string id,
            Uri uri,
            Uri href,
            string? displayName,
            IReadOnlyList<Image> images,
            Followers followers,
            IReadOnlyDictionary<string, Uri> externalUrls) :
            base(id, uri)
        {
            this.Href = href;
            this.DisplayName = displayName;
            this.Images = new ImmutableValueArray<Image>(images);
            this.Followers = followers;
            this.ExternalUrls = new ImmutableValueDictionary<string, Uri>(externalUrls);
        }

        /// <summary>
        /// Gets or sets a link to the Spotify Web API endpoint providing full details of <see cref="PublicUser"/>.
        /// </summary>
        /// <returns>A link to the Spotify Web API endpoint providing full details of the <see cref="PublicUser"/>.</returns>
        public Uri Href { get; init; }
        /// <summary>
        /// Gets or sets the display name of the <see cref="PublicUser"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="string"/> representing the display name of the <see cref="PublicUser"/>,
        /// or <see langword="null"/> if not available.
        /// </returns>
        public string? DisplayName { get; init; }
        /// <summary>
        /// Gets or sets the profile image of the <see cref="PublicUser"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="IReadOnlyList{T}"/> of <see cref="Image"/> objects
        /// representing the profile image of the <see cref="PublicUser"/> in various sizes.
        /// </returns>
        public IReadOnlyList<Image> Images { get; init; }
        /// <summary>
        /// Gets or sets the <see cref="ObjectModel.Followers"/> of the <see cref="PublicUser"/>.
        /// </summary>
        /// <returns>The <see cref="ObjectModel.Followers"/> of the <see cref="PublicUser"/>.</returns>
        public Followers Followers { get; init; }
        /// <summary>
        /// Gets or sets the known external URLs for the <see cref="PublicUser"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="IReadOnlyDictionary{TKey, TValue}"/> containing the known
        /// external URLs for the <see cref="PublicUser"/>, keyed by the type of the URL.
        /// </returns>
        public IReadOnlyDictionary<string, Uri> ExternalUrls { get; init; }
    }
}