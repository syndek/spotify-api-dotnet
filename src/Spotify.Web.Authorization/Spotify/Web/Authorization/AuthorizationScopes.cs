using System;

namespace Spotify.Web.Authorization
{
    /// <summary>
    /// Defines different <see href="https://spotify.dev/documentation/general/guides/authorization-guide/#scopes">authorization scopes</see>,
    /// which allow applications to access specific Spotify Web API endpoints on behalf of a user.
    /// </summary>
    [Flags]
    public enum AuthorizationScopes
    {
        /// <summary>
        /// Provides write access to user-provided images.
        /// </summary>
        UgcImageUpload = 0x01,
        /// <summary>
        /// Provides read access to a user's player state.
        /// </summary>
        UserReadPlaybackState = 0x02,
        /// <summary>
        /// Provides write access to a user's player state.
        /// </summary>
        UserModifyPlaybackState = 0x04,
        /// <summary>
        /// Provides read access to a user's currently playing content.
        /// </summary>
        UserReadCurrentlyPlaying = 0x08,
        /// <summary>
        /// Provides control of playback of a Spotify track.
        /// </summary>
        Streaming = 0x10,
        /// <summary>
        /// Provides remote control playback of Spotify.
        /// </summary>
        AppRemoteControl = 0x20,
        /// <summary>
        /// Provides read access to a user's email address.
        /// </summary>
        UserReadEmail = 0x40,
        /// <summary>
        /// Provides read access to a user's subscription details (type of user account).
        /// </summary>
        UserReadPrivate = 0x80,
        /// <summary>
        /// Provides the inclusion of collaborative playlists when requesting a user's playlists.
        /// </summary>
        PlaylistReadCollaborative = 0x0100,
        /// <summary>
        /// Provides write access to a user's public playlists.
        /// </summary>
        PlaylistModifyPublic = 0x0200,
        /// <summary>
        /// Provides read access to a user's private playlists.
        /// </summary>
        PlaylistReadPrivate = 0x0400,
        /// <summary>
        /// Provides write access to a user's private playlists.
        /// </summary>
        PlaylistModifyPrivate = 0x0800,
        /// <summary>
        /// Provides write/delete access to a user's music library.
        /// </summary>
        UserLibraryModify = 0x1000,
        /// <summary>
        /// Provides read access to a user's music library.
        /// </summary>
        UserLibraryRead = 0x2000,
        /// <summary>
        /// Provides read access to a user's top artists and tracks.
        /// </summary>
        UserTopRead = 0x4000,
        /// <summary>
        /// Provides read access to a user's playback position within a piece of content.
        /// </summary>
        UserReadPlaybackPosition = 0x8000,
        /// <summary>
        /// Provides read access to a user's recently played tracks.
        /// </summary>
        UserReadRecentlyPlayed = 0x010000,
        /// <summary>
        /// Provides read access to the list of artists and other users that a user follows.
        /// </summary>
        UserFollowRead = 0x020000,
        /// <summary>
        /// Provides write/delete access to the list of artists and other users that a user follows.
        /// </summary>
        UserFollowModify = 0x040000
    }
}
