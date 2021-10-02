namespace Spotify.ObjectModel
{
    /// <summary>
    /// Defines different ranges of time recognised by the Spotify Web API.
    /// </summary>
    public enum TimeRange
    {
        /// <summary>
        /// Approximately the last 4 weeks.
        /// </summary>
        ShortTerm,
        /// <summary>
        /// Approximately the last 6 months.
        /// </summary>
        MediumTerm,
        /// <summary>
        /// Several years, including new data as it becomes available.
        /// </summary>
        LongTerm
    }
}
