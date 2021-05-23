namespace Spotify.ObjectModel
{
    /// <summary>
    /// Defines the precisions with which a release date value can be known.
    /// </summary>
    public enum ReleaseDatePrecision
    {
        /// <summary>
        /// Only the year of the release date is known.
        /// </summary>
        Year,
        /// <summary>
        /// Only the month and year of the release date are known.
        /// </summary>
        Month,
        /// <summary>
        /// The day, month, and year of the release date are all known.
        /// </summary>
        Day
    }
}
