using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Spotify.ObjectModel;
using Spotify.Web.Authorization;

namespace Spotify.Web
{
    /// <summary>
    /// Represents a <see cref="SpotifyApiClient"/> for managing and retrieving information about a user’s playlists.
    /// </summary>
    public class SpotifyPlaylistsApiClient : SpotifyApiClient, ISpotifyPlaylistsApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpotifyPlaylistsApiClient"/> class with the specified <paramref name="httpClient"/>.
        /// </summary>
        /// <param name="httpClient">An instance of <see cref="HttpClient"/> to use for requests to the Spotify Web API.</param>
        public SpotifyPlaylistsApiClient(HttpClient httpClient) : base(httpClient) { }

        /// <inheritdoc/>
        public Task CreatePlaylistAsync(
            String id,
            String name,
            String? description = null,
            Boolean? publicPlaylist = null,
            Boolean? collaborative = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<Playlist> GetPlaylistAsync(
            String id,
            CountryCode? market = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<Paging<IPlayable>> GetPlaylistItemsAsync(
            String id,
            Int32? limit = null,
            Int32? offset = null,
            CountryCode? market = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<Paging<Playlist>> GetCurrentUserPlaylistsAsync(
            Int32? limit = null,
            Int32? offset = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<Paging<Playlist>> GetUserPlaylistsAsync(
            String id,
            Int32? limit = null,
            Int32? offset = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task ChangePlaylistDetailsAsync(
            String id,
            String? name = null,
            String? description = null,
            Boolean? publicPlaylist = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<IReadOnlyList<Image>> GetPlaylistCoverImageAsync(
            String id,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task SetPlaylistCoverImageAsync(
            String id,
            String base64Image,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task ReorderPlaylistItemsAsync(
            String id,
            Int32 rangeStart,
            Int32 insertBefore,
            Int32? rangeLength = null,
            String? snapshotId = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task ReplacePlaylistItemsAsync(
            String id,
            IEnumerable<String> uris,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task AddItemsToPlaylistAsync(
            String id,
            IEnumerable<String> uris,
            Int32? position = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task RemoveItemsFromPlaylistAsync(
            String id,
            IEnumerable<String> uris,
            String? snapshotId = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}