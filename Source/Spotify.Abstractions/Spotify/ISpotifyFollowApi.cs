using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Spotify
{
    /// <summary>
    /// Defines methods for managing the artists, users and playlists that a Spotify user follows.
    /// </summary>
    public interface ISpotifyFollowApi
    {
        Task<IReadOnlyList<Boolean>> CheckCurrentUserFollowsArtistsAsync(
            IEnumerable<String> ids,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Boolean>> CheckCurrentUserFollowsUsersAsync(
            IEnumerable<String> ids,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Boolean>> CheckUsersFollowPlaylistAsync(
            String id,
            IEnumerable<String> userIds,
            CancellationToken cancellationToken = default);

        Task FollowArtistsAsync(
            IEnumerable<String> ids,
            CancellationToken cancellationToken = default);

        Task FollowUsersAsync(
            IEnumerable<String> ids,
            CancellationToken cancellationToken = default);

        Task FollowPlaylistAsync(
            String id,
            Boolean? publicFollow = false,
            CancellationToken cancellationToken = default);

        Task UnfollowArtistsAsync(
            IEnumerable<String> ids,
            CancellationToken cancellationToken = default);

        Task UnfollowUsersAsync(
            IEnumerable<String> ids,
            CancellationToken cancellationToken = default);

        Task UnfollowPlaylistAsync(
            String id,
            CancellationToken cancellationToken = default);
    }
}