using System;

namespace Spotify.ObjectModel
{
    /// <summary>
    /// Represents an image.
    /// </summary>
    public record Image : SpotifyObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Image"/> record with the specified values.
        /// </summary>
        /// <param name="url">The source URL of the image.</param>
        /// <param name="width">The width in pixels of the image, or <see langword="null"/> if unknown.</param>
        /// <param name="height">The height in pixels of the image, or <see langword="null"/> if unknown.</param>
        public Image(Uri url, int? width, int? height)
        {
            Url = url;
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Gets or sets the source URL of the <see cref="Image"/>.
        /// </summary>
        /// <returns>The source URL of the <see cref="Image"/>.</returns>
        public Uri Url { get; init; }

        /// <summary>
        /// Gets or sets the width in pixels of the <see cref="Image"/>.
        /// </summary>
        /// <returns>The width in pixels of the <see cref="Image"/>, or <see langword="null"/> if unknown.</returns>
        public int? Width { get; init; }

        /// <summary>
        /// Gets or sets the height in pixels of the <see cref="Image"/>.
        /// </summary>
        /// <returns>The height in pixels of the <see cref="Image"/>, or <see langword="null"/> if unknown.</returns>
        public int? Height { get; init; }
    }
}
