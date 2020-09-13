using System;

namespace Spotify.ObjectModel
{
    /// <summary>
    /// Defines the different modalities of a track (the type of scale from which its melodic content is derived).
    /// </summary>
    public enum Modality : Int32
    {
        /// <summary>
        /// The minor mode.
        /// </summary>
        Minor,
        /// <summary>
        /// The major mode.
        /// </summary>
        Major
    }
}