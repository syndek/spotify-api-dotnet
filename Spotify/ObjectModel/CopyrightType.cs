using System;

namespace Spotify.ObjectModel
{
    /// <summary>
    /// Defines different types of <see cref="ObjectModel.Copyright"/>.
    /// </summary>
    public enum CopyrightType : Int32
    {
        /// <summary>
        /// The <see cref="ObjectModel.Copyright"/> is the regular copyright.
        /// </summary>
        Copyright,
        /// <summary>
        /// The <see cref="ObjectModel.Copyright"/> is the sound recording (performance) copyright.
        /// </summary>
        PerformanceCopyright
    }
}