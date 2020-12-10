using System;

namespace Spotify.ObjectModel
{
    /// <summary>
    /// Defines different Spotify subscription levels.
    /// </summary>
    public enum Product : int
    {
        /// <summary>
        /// The Spotify Free plan (can be considered the same as 'open').
        /// </summary>
        Free,
        /// <summary>
        /// The Spotify Premium (paid) plan.
        /// </summary>
        Premium
    }
}