namespace Spotify.ObjectModel
{
    /// <summary>
    /// Represents a user's most recent playback position within an <see cref="Episode"/>.
    /// </summary>
    public record ResumePoint : SpotifyObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResumePoint"/> record with the specified values.
        /// </summary>
        /// <param name="resumePosition">
        /// An <see cref="int"/> representing the user's most recent playback position in the <see cref="Episode"/> in milliseconds.
        /// </param>
        /// <param name="isFullyPlayed">
        /// A <see cref="bool"/> representing whether or not the <see cref="Episode"/> has been fully played by the user.
        /// </param>
        public ResumePoint(int resumePosition, bool isFullyPlayed) : base()
        {
            ResumePosition = resumePosition;
            IsFullyPlayed = isFullyPlayed;
        }

        /// <summary>
        /// Gets or sets the resume position of the <see cref="ResumePoint"/>.
        /// </summary>
        /// <returns>
        /// An <see cref="int"/> representing the user's most recent playback position in the <see cref="Episode"/> in milliseconds.
        /// </returns>
        public int ResumePosition { get; init; }
        /// <summary>
        /// Gets or sets a value representing whether or not the <see cref="Episode"/> has been fully played by the user.
        /// </summary>
        /// <returns>
        /// A <see cref="bool"/> representing whether or not the <see cref="Episode"/> has been fully played by the user.
        /// </returns>
        public bool IsFullyPlayed { get; init; }
    }
}