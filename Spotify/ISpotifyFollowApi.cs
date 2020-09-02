using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Spotify.Web.Authorization;

namespace Spotify
{
    /// <summary>
    /// Defines methods for managing the artists, users and playlists that a Spotify user follows.
    /// </summary>
    public interface ISpotifyFollowApi
    {
        Task<IReadOnlyList<Boolean>> CheckCurrentUserFollowsArtistsAsync(
            IEnumerable<String> ids,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Boolean>> CheckCurrentUserFollowsUsersAsync(
            IEnumerable<String> ids,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Boolean>> CheckUsersFollowPlaylistAsync(
            String playlistId,
            IEnumerable<String> ids,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default);

        Task FollowArtistsAsync(
            IEnumerable<String> ids,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default);

        Task FollowUsersAsync(
            IEnumerable<String> ids,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default);

        Task FollowPlaylistAsync(
            String id,
            Boolean? publicFollow = false,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default);

        Task UnfollowArtistsAsync(
            IEnumerable<String> ids,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default);

        Task UnfollowUsersAsync(
            IEnumerable<String> ids,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default);

        Task UnfollowPlaylistAsync(
            String id,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default);
    }
}