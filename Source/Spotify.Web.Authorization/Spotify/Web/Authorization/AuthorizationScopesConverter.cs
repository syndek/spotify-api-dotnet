using System;
using System.Collections.Generic;
using System.Linq;

namespace Spotify.Web.Authorization
{
    internal static class AuthorizationScopesConverter
    {
        internal static AuthorizationScopes FromSpotifyString(String authorizationScope) => authorizationScope switch
        {
            "ugc-image-upload" => AuthorizationScopes.UgcImageUpload,
            "user-read-playback-state" => AuthorizationScopes.UserReadPlaybackState,
            "user-modify-playback-state" => AuthorizationScopes.UserModifyPlaybackState,
            "user-read-currently-playing" => AuthorizationScopes.UserReadCurrentlyPlaying,
            "streaming" => AuthorizationScopes.Streaming,
            "app-remote-control" => AuthorizationScopes.AppRemoteControl,
            "user-read-email" => AuthorizationScopes.UserReadEmail,
            "user-read-private" => AuthorizationScopes.UserReadPrivate,
            "playlist-read-collaborative" => AuthorizationScopes.PlaylistReadCollaborative,
            "playlist-modify-public" => AuthorizationScopes.PlaylistModifyPublic,
            "playlist-read-private" => AuthorizationScopes.PlaylistReadPrivate,
            "playlist-modify-private" => AuthorizationScopes.PlaylistModifyPrivate,
            "user-library-modify" => AuthorizationScopes.UserLibraryModify,
            "user-library-read" => AuthorizationScopes.UserLibraryRead,
            "user-top-read" => AuthorizationScopes.UserTopRead,
            "user-read-playback-position" => AuthorizationScopes.UserReadPlaybackPosition,
            "user-read-recently-played" => AuthorizationScopes.UserReadRecentlyPlayed,
            "user-follow-read" => AuthorizationScopes.UserFollowRead,
            "user-follow-modify" => AuthorizationScopes.UserFollowModify,
            _ => throw new ArgumentException($"Invalid {nameof(AuthorizationScopes)} string value: {authorizationScope}", nameof(authorizationScope))
        };

        internal static AuthorizationScopes FromSpotifyStrings(IEnumerable<String> authorizationScopes) =>
            authorizationScopes.Aggregate(
                new AuthorizationScopes(),
                (current, authorizationScope) => current | AuthorizationScopesConverter.FromSpotifyString(authorizationScope));

        internal static String ToSpotifyString(this AuthorizationScopes authorizationScope) => authorizationScope switch
        {
            AuthorizationScopes.UgcImageUpload => "ugc-image-upload",
            AuthorizationScopes.UserReadPlaybackState => "user-read-playback-state",
            AuthorizationScopes.UserModifyPlaybackState => "user-modify-playback-state",
            AuthorizationScopes.UserReadCurrentlyPlaying => "user-read-currently-playing",
            AuthorizationScopes.Streaming => "streaming",
            AuthorizationScopes.AppRemoteControl => "app-remote-control",
            AuthorizationScopes.UserReadEmail => "user-read-email",
            AuthorizationScopes.UserReadPrivate => "user-read-private",
            AuthorizationScopes.PlaylistReadCollaborative => "playlist-read-collaborative",
            AuthorizationScopes.PlaylistModifyPublic => "playlist-modify-public",
            AuthorizationScopes.PlaylistReadPrivate => "playlist-read-private",
            AuthorizationScopes.PlaylistModifyPrivate => "playlist-modify-private",
            AuthorizationScopes.UserLibraryModify => "user-library-modify",
            AuthorizationScopes.UserLibraryRead => "user-library-read",
            AuthorizationScopes.UserTopRead => "user-top-read",
            AuthorizationScopes.UserReadPlaybackPosition => "user-read-playback-position",
            AuthorizationScopes.UserReadRecentlyPlayed => "user-read-recently-played",
            AuthorizationScopes.UserFollowRead => "user-follow-read",
            AuthorizationScopes.UserFollowModify => "user-follow-modify",
            _ => throw new ArgumentException($"Invalid {nameof(AuthorizationScopes)} value: {authorizationScope}", nameof(authorizationScope))
        };

        internal static IEnumerable<String> ToSpotifyStrings(this AuthorizationScopes authorizationScopes) =>
            from value in Enum.GetValues<AuthorizationScopes>() where authorizationScopes.HasFlag(value) select value.ToSpotifyString();
    }
}