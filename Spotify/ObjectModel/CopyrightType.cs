using System;

namespace Spotify.ObjectModel
{
    /// <summary>
    /// Defines different types of <see cref="ObjectModel.Copyright"/>.
    /// </summary>
    public enum CopyrightType : Int32
    {
        /// <summary>
        /// The copyright.
        /// </summary>
        Copyright,
        /// <summary>
        /// The sound recording (performance) copyright.
        /// </summary>
        PerformanceCopyright
    }
}