using System;

namespace Spotify.ObjectModel
{
    /// <summary>
    /// Represents data about a saved track/show/album.
    /// </summary>
    /// <typeparam name="TSaved">The type of saved object.</typeparam>
    public record Saved<TSaved> : SpotifyObject where TSaved : ISaveable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Saved{TSaved}"/> record with the specified values.
        /// </summary>
        /// <param name="savedObject">An object of type <typeparamref name="TSaved"/> representing the object that was saved.</param>
        /// <param name="savedAt">The <see cref="DateTime"/> at which <paramref name="savedObject"/> was saved.</param>
        public Saved(TSaved savedObject, DateTime savedAt)
        {
            SavedObject = savedObject;
            SavedAt = savedAt;
        }

        /// <summary>
        /// Gets or sets the object that was saved.
        /// </summary>
        /// <returns>An object of type <typeparamref name="TSaved"/> representing the object that was saved.</returns>
        public TSaved SavedObject { get; init; }

        /// <summary>
        /// Gets or sets the <see cref="DateTime"/> at which <see cref="SavedObject"/> was saved.
        /// </summary>
        /// <returns>
        /// The <see cref="DateTime"/> at which <see cref="SavedObject"/> was saved.
        /// </returns>
        public DateTime SavedAt { get; init; }
    }
}