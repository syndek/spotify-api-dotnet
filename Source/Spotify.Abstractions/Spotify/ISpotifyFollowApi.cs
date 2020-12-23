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
        Task<IReadOnlyList<bool>> CheckCurrentUserFollowsArtistsAsync(
            IEnumerable<string> ids,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<bool>> CheckCurrentUserFollowsUsersAsync(
            IEnumerable<string> ids,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<bool>> CheckUsersFollowPlaylistAsync(
            string id,
            IEnumerable<string> userIds,
            CancellationToken cancellationToken = default);

        Task FollowArtistsAsync(
            IEnumerable<string> ids,
            CancellationToken cancellationToken = default);

        Task FollowUsersAsync(
            IEnumerable<string> ids,
            CancellationToken cancellationToken = default);

        Task FollowPlaylistAsync(
            string id,
            bool? publicFollow = false,
            CancellationToken cancellationToken = default);

        Task UnfollowArtistsAsync(
            IEnumerable<string> ids,
            CancellationToken cancellationToken = default);

        Task UnfollowUsersAsync(
            IEnumerable<string> ids,
            CancellationToken cancellationToken = default);

        Task UnfollowPlaylistAsync(
            string id,
            CancellationToken cancellationToken = default);
    }
}
