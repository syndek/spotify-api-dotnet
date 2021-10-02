using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Spotify.ObjectModel;
using Spotify.ObjectModel.Collections;

namespace Spotify
{
    /// <summary>
    /// Defines methods for managing and retrieving information about a user’s playlists.
    /// </summary>
    public interface ISpotifyPlaylistsApi
    {
        Task<Playlist> CreatePlaylistAsync(
            string userId,
            string name,
            string? description = null,
            bool? isPublic = null,
            bool? isCollaborative = null,
            CancellationToken cancellationToken = default);

        Task<Playlist> GetPlaylistAsync(
            string id,
            CountryCode? market = null,
            CancellationToken cancellationToken = default);

        Task<Paging<IPlayable>> GetPlaylistItemsAsync(
            string id,
            int? limit = null,
            int? offset = null,
            CountryCode? market = null,
            CancellationToken cancellationToken = default);

        Task<Paging<SimplifiedPlaylist>> GetCurrentUserPlaylistsAsync(
            int? limit = null,
            int? offset = null,
            CancellationToken cancellationToken = default);

        Task<Paging<SimplifiedPlaylist>> GetUserPlaylistsAsync(
            string userId,
            int? limit = null,
            int? offset = null,
            CancellationToken cancellationToken = default);

        Task ChangePlaylistDetailsAsync(
            string id,
            string? name = null,
            string? description = null,
            bool? isPublic = null,
            bool? isCollaborative = null,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Image>> GetPlaylistCoverImageAsync(
            string id,
            CancellationToken cancellationToken = default);

        Task SetPlaylistCoverImageAsync(
            string id,
            string base64Image,
            CancellationToken cancellationToken = default);

        Task<string> ReorderPlaylistItemsAsync(
            string id,
            int rangeStart,
            int insertBefore,
            int? rangeLength = null,
            string? snapshotId = null,
            CancellationToken cancellationToken = default);

        Task ReplacePlaylistItemsAsync(
            string id,
            IEnumerable<string> uris,
            CancellationToken cancellationToken = default);

        Task<string> AddItemsToPlaylistAsync(
            string id,
            IEnumerable<string> uris,
            int? position = null,
            CancellationToken cancellationToken = default);

        Task<string> RemoveItemsFromPlaylistAsync(
            string id,
            IEnumerable<string> uris,
            string? snapshotId = null,
            CancellationToken cancellationToken = default);
    }
}
