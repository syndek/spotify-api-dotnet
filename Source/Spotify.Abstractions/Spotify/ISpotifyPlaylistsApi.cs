using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Spotify.ObjectModel;

namespace Spotify
{
    /// <summary>
    /// Defines methods for managing and retrieving information about a user’s playlists.
    /// </summary>
    public interface ISpotifyPlaylistsApi
    {
        Task<Playlist> CreatePlaylistAsync(
            String userId,
            String name,
            String? description = null,
            Boolean? isPublic = null,
            Boolean? isCollaborative = null,
            CancellationToken cancellationToken = default);

        Task<Playlist> GetPlaylistAsync(String id, CountryCode? market = null, CancellationToken cancellationToken = default);

        Task<Paging<IPlayable>> GetPlaylistItemsAsync(
            String id,
            Int32? limit = null,
            Int32? offset = null,
            CountryCode? market = null,
            CancellationToken cancellationToken = default);

        Task<Paging<SimplifiedPlaylist>> GetCurrentUserPlaylistsAsync(
            Int32? limit = null,
            Int32? offset = null,
            CancellationToken cancellationToken = default);

        Task<Paging<SimplifiedPlaylist>> GetUserPlaylistsAsync(
            String userId,
            Int32? limit = null,
            Int32? offset = null,
            CancellationToken cancellationToken = default);

        Task ChangePlaylistDetailsAsync(
            String id,
            String? name = null,
            String? description = null,
            Boolean? isPublic = null,
            Boolean? isCollaborative = null,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Image>> GetPlaylistCoverImageAsync(String id, CancellationToken cancellationToken = default);

        Task SetPlaylistCoverImageAsync(String id, String base64Image, CancellationToken cancellationToken = default);

        Task<String> ReorderPlaylistItemsAsync(
            String id,
            Int32 rangeStart,
            Int32 insertBefore,
            Int32? rangeLength = null,
            String? snapshotId = null,
            CancellationToken cancellationToken = default);

        Task ReplacePlaylistItemsAsync(String id, IEnumerable<String> uris, CancellationToken cancellationToken = default);

        Task<String> AddItemsToPlaylistAsync(
            String id,
            IEnumerable<String> uris,
            Int32? position = null,
            CancellationToken cancellationToken = default);

        Task<String> RemoveItemsFromPlaylistAsync(
            String id,
            IEnumerable<String> uris,
            String? snapshotId = null,
            CancellationToken cancellationToken = default);
    }
}