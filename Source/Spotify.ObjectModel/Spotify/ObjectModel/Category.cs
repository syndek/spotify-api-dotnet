using Spotify.ObjectModel.Collections;
using System;
using System.Collections.Generic;

namespace Spotify.ObjectModel
{
    /// <summary>
    /// Represents a Spotify category.
    /// </summary>
    public record Category : IdentifiableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Category"/> record with the specified values.
        /// </summary>
        /// <param name="id">A <see cref="string"/> representing the Spotify ID of the category.</param>
        /// <param name="href">A link to the Spotify Web API endpoint returning full details of the category.</param>
        /// <param name="name">A <see cref="string"/> representing the name of the category.</param>
        /// <param name="icons">A <see cref="IReadOnlyList{T}"/> of the category's icon in various sizes.</param>
        public Category(string id, Uri href, string name, IReadOnlyList<Image> icons) : base(id)
        {
            Href = href;
            Name = name;
            Icons = new ImmutableValueArray<Image>(icons);
        }

        /// <summary>
        /// Gets or sets a link to the Spotify Web API endpoint returning full details of the <see cref="Category"/>.
        /// </summary>
        /// <returns>A link to the Spotify Web API endpoint returning full details of the <see cref="Category"/>.</returns>
        public Uri Href { get; init; }
        /// <summary>
        /// Gets or sets the name of the <see cref="Category"/>.
        /// </summary>
        /// <returns>A <see cref="string"/> representing the name of the <see cref="Category"/>.</returns>
        public string Name { get; init; }
        /// <summary>
        /// Gets or sets the <see cref="Category"/>'s icon in various sizes.
        /// </summary>
        /// <returns>A <see cref="IReadOnlyList{T}"/> of the <see cref="Category"/>'s icon in various sizes.</returns>
        public IReadOnlyList<Image> Icons { get; init; }
    }
}