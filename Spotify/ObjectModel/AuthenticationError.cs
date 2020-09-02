using System;

namespace Spotify.ObjectModel
{
    internal sealed record AuthenticationError : SpotifyObject
    {
        internal AuthenticationError(String error, String? errorDescription) : base()
        {
            this.Error = error;
            this.ErrorDescription = errorDescription;
        }

        internal String Error { get; init; }
        internal String? ErrorDescription { get; init; }
    }
}