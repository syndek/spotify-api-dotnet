using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Spotify.ObjectModel;
using Spotify.Web.Authorization;

namespace Spotify
{
    /// <summary>
    /// Defines methods for managing and retrieving information about a user’s playlists.
    /// </summary>
    public interface ISpotifyPlaylistsApi
    {
        Task CreatePlaylistAsync(
            String id,
            String name,
            String? description = null,
            Boolean? publicPlaylist = null,
            Boolean? collaborative = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default);

        Task<Playlist> GetPlaylistAsync(
            String id,
            CountryCode? market = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default);

        Task<Paging<IPlayable>> GetPlaylistItemsAsync(
            String id,
            Int32? limit = null,
            Int32? offset = null,
            CountryCode? market = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default);

        Task<Paging<Playlist>> GetCurrentUserPlaylistsAsync(
            Int32? limit = null,
            Int32? offset = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default);

        Task<Paging<Playlist>> GetUserPlaylistsAsync(
            String id,
            Int32? limit = null,
            Int32? offset = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default);

        Task ChangePlaylistDetailsAsync(
            String id,
            String? name = null,
            String? description = null,
            Boolean? publicPlaylist = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Image>> GetPlaylistCoverImageAsync(
            String id,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default);

        Task SetPlaylistCoverImageAsync(
            String id,
            String base64Image,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default);

        Task ReorderPlaylistItemsAsync(
            String id,
            Int32 rangeStart,
            Int32 insertBefore,
            Int32? rangeLength = null,
            String? snapshotId = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default);

        Task ReplacePlaylistItemsAsync(
            String id,
            IEnumerable<String> uris,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default);

        Task AddItemsToPlaylistAsync(
            String id,
            IEnumerable<String> uris,
            Int32? position = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default);

        Task RemoveItemsFromPlaylistAsync(
            String id,
            IEnumerable<String> uris,
            String? snapshotId = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default);
    }
}