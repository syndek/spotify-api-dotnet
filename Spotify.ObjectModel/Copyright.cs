using System;

namespace Spotify.ObjectModel
{
    /// <summary>
    /// Represents a copyright.
    /// </summary>
    public record Copyright : SpotifyObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Copyright"/> record with the specified values.
        /// </summary>
        /// <param name="text">The text of the copyright.</param>
        /// <param name="type">The type of the copyright.</param>
        public Copyright(String text, CopyrightType type) : base()
        {
            this.Text = text;
            this.Type = type;
        }

        /// <summary>
        /// Gets or sets the text of the <see cref="Copyright"/>.
        /// </summary>
        /// <returns>The text of the <see cref="Copyright"/>.</returns>
        public String Text { get; init; }
        /// <summary>
        /// Gets or sets the type of the <see cref="Copyright"/>.
        /// </summary>
        /// <returns>The type of the <see cref="Copyright"/>.</returns>
        public CopyrightType Type { get; init; }
    }
}