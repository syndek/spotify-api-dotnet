using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using Spotify.ObjectModel;
using Spotify.Web.Authorization;
using Spotify.Web.RequestObjects;

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

        public Task<Playlist> CreatePlaylistAsync(
            String userId,
            String name,
            String? description = null,
            Boolean? isPublic = null,
            Boolean? isCollaborative = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            return base.SendAsync<Playlist>(
                new($"{SpotifyApiClient.BaseUrl}/users/{userId}/playlists"),
                HttpMethod.Post,
                new StringContent(
                    JsonSerializer.Serialize(
                        new PlaylistDetails(name, description, isPublic, isCollaborative),
                        SpotifyApiClient.RequestObjectSerializerOptions),
                    Encoding.UTF8,
                    MediaTypeNames.Application.Json),
                accessTokenProvider,
                cancellationToken);
        }

        public Task<Playlist> GetPlaylistAsync(
            String id,
            CountryCode? market = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            var uriBuilder = new SpotifyUriBuilder($"{SpotifyApiClient.BaseUrl}/playlists/{id}")
                .AppendToQueryIfNotNull("market", market)
                .AppendToQuery("additional_types", "episode");

            return base.SendAsync<Playlist>(
                uriBuilder.Build(),
                HttpMethod.Get,
                content: null,
                accessTokenProvider,
                cancellationToken);
        }

        public Task<Paging<IPlayable>> GetPlaylistItemsAsync(
            String id,
            Int32? limit = null,
            Int32? offset = null,
            CountryCode? market = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            var uriBuilder = new SpotifyUriBuilder($"{SpotifyApiClient.BaseUrl}/playlists/{id}")
                .AppendToQueryIfNotNull("limit", limit)
                .AppendToQueryIfNotNull("offset", offset)
                .AppendToQueryIfNotNull("market", market)
                .AppendToQuery("additional_types", "episode");

            return base.SendAsync<Paging<IPlayable>>(
                uriBuilder.Build(),
                HttpMethod.Get,
                content: null,
                accessTokenProvider,
                cancellationToken);
        }

        public Task<Paging<SimplifiedPlaylist>> GetCurrentUserPlaylistsAsync(
            Int32? limit = null,
            Int32? offset = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            var uriBuilder = new SpotifyUriBuilder($"{SpotifyApiClient.BaseUrl}/me/playlists")
                .AppendToQueryIfNotNull("limit", limit)
                .AppendToQueryIfNotNull("offset", offset);

            return base.SendAsync<Paging<SimplifiedPlaylist>>(
                uriBuilder.Build(),
                HttpMethod.Get,
                content: null,
                accessTokenProvider,
                cancellationToken);
        }

        public Task<Paging<SimplifiedPlaylist>> GetUserPlaylistsAsync(
            String userId,
            Int32? limit = null,
            Int32? offset = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            var uriBuilder = new SpotifyUriBuilder($"{SpotifyApiClient.BaseUrl}/users/{userId}/playlists")
                .AppendToQueryIfNotNull("limit", limit)
                .AppendToQueryIfNotNull("offset", offset);

            return base.SendAsync<Paging<SimplifiedPlaylist>>(
                uriBuilder.Build(),
                HttpMethod.Get,
                content: null,
                accessTokenProvider,
                cancellationToken);
        }

        public Task ChangePlaylistDetailsAsync(
            String id,
            String? name = null,
            String? description = null,
            Boolean? isPublic = null,
            Boolean? isCollaborative = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            return base.SendAsync(
                new($"{SpotifyApiClient.BaseUrl}/playlists/{id}"),
                HttpMethod.Put,
                new StringContent(
                    JsonSerializer.Serialize(new PlaylistDetails(name, description, isPublic, isCollaborative),
                    SpotifyApiClient.RequestObjectSerializerOptions),
                    Encoding.UTF8,
                    MediaTypeNames.Application.Json),
                accessTokenProvider,
                cancellationToken);
        }

        public Task<IReadOnlyList<Image>> GetPlaylistCoverImageAsync(
            String id,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            return base.SendAsync<IReadOnlyList<Image>>(
                new($"{SpotifyApiClient.BaseUrl}/playlists/{id}/images"),
                HttpMethod.Get,
                content: null,
                accessTokenProvider,
                cancellationToken);
        }

        public Task SetPlaylistCoverImageAsync(
            String id,
            String base64Image,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            return base.SendAsync<IReadOnlyList<Image>>(
                new($"{SpotifyApiClient.BaseUrl}/playlists/{id}/images"),
                HttpMethod.Put,
                new StringContent(base64Image, Encoding.UTF8, MediaTypeNames.Image.Jpeg),
                accessTokenProvider,
                cancellationToken);
        }

        public Task<String> ReorderPlaylistItemsAsync(
            String id,
            Int32 rangeStart,
            Int32 insertBefore,
            Int32? rangeLength = null,
            String? snapshotId = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            // TODO: Find a way to make this deserialize a SnapshotId object.
            return base.SendAsync<String>(
                new($"{SpotifyApiClient.BaseUrl}/playlists/{id}/tracks"),
                HttpMethod.Put,
                new StringContent(
                    JsonSerializer.Serialize(
                        new ReorderPlaylistItemsParameters(rangeStart, insertBefore, rangeLength, snapshotId),
                        SpotifyApiClient.RequestObjectSerializerOptions),
                    Encoding.UTF8,
                    MediaTypeNames.Application.Json),
                accessTokenProvider,
                cancellationToken);
        }

        public Task ReplacePlaylistItemsAsync(
            String id,
            IEnumerable<String> uris,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            var uriBuilder = new SpotifyUriBuilder($"{SpotifyApiClient.BaseUrl}/playlists/{id}/tracks")
                .AppendJoinToQuery("uris", ',', uris);

            return base.SendAsync(
                uriBuilder.Build(),
                HttpMethod.Put,
                content: null,
                accessTokenProvider,
                cancellationToken);
        }

        public Task<String> AddItemsToPlaylistAsync(
            String id,
            IEnumerable<String> uris,
            Int32? position = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            var uriBuilder = new SpotifyUriBuilder($"{SpotifyApiClient.BaseUrl}/playlists/{id}")
                .AppendJoinToQuery("uris", ',', uris)
                .AppendToQueryIfNotNull("position", position);

            // TODO: Find a way to make this deserialize a SnapshotId object.
            return base.SendAsync<String>(
                uriBuilder.Build(),
                HttpMethod.Post,
                content: null,
                accessTokenProvider,
                cancellationToken);
        }

        public Task<String> RemoveItemsFromPlaylistAsync(
            String id,
            IEnumerable<String> uris,
            String? snapshotId = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            // TODO: Implement with respect to https://spotify.dev/documentation/web-api/reference/playlists/remove-tracks-playlist.
            throw new NotImplementedException();
        }

        #region ISpotifyPlaylistsApi Implementation
        Task<Playlist> ISpotifyPlaylistsApi.CreatePlaylistAsync(
            String userId,
            String name,
            String? description,
            Boolean? isPublic,
            Boolean? isCollaborative,
            CancellationToken cancellationToken)
        {
            return this.CreatePlaylistAsync(userId, name, description, isPublic, isCollaborative, null, cancellationToken);
        }

        Task<Playlist> ISpotifyPlaylistsApi.GetPlaylistAsync(String id, CountryCode? market, CancellationToken cancellationToken)
        {
            return this.GetPlaylistAsync(id, market, null, cancellationToken);
        }

        Task<Paging<IPlayable>> ISpotifyPlaylistsApi.GetPlaylistItemsAsync(
            String id,
            Int32? limit,
            Int32? offset,
            CountryCode? market,
            CancellationToken cancellationToken)
        {
            return this.GetPlaylistItemsAsync(id, limit, offset, market, null, cancellationToken);
        }

        Task<Paging<SimplifiedPlaylist>> ISpotifyPlaylistsApi.GetCurrentUserPlaylistsAsync(
            Int32? limit,
            Int32? offset,
            CancellationToken cancellationToken)
        {
            return this.GetCurrentUserPlaylistsAsync(limit, offset, null, cancellationToken);
        }

        Task<Paging<SimplifiedPlaylist>> ISpotifyPlaylistsApi.GetUserPlaylistsAsync(
            String userId,
            Int32? limit,
            Int32? offset,
            CancellationToken cancellationToken)
        {
            return this.GetUserPlaylistsAsync(userId, limit, offset, null, cancellationToken);
        }

        Task ISpotifyPlaylistsApi.ChangePlaylistDetailsAsync(
            String id,
            String? name,
            String? description,
            Boolean? isPublic,
            Boolean? isCollaborative,
            CancellationToken cancellationToken)
        {
            return this.ChangePlaylistDetailsAsync(id, name, description, isPublic, isCollaborative, null, cancellationToken);
        }

        Task<IReadOnlyList<Image>> ISpotifyPlaylistsApi.GetPlaylistCoverImageAsync(String id, CancellationToken cancellationToken)
        {
            return this.GetPlaylistCoverImageAsync(id, null, cancellationToken);
        }

        Task ISpotifyPlaylistsApi.SetPlaylistCoverImageAsync(String id, String base64Image, CancellationToken cancellationToken)
        {
            return this.SetPlaylistCoverImageAsync(id, base64Image, null, cancellationToken);
        }

        Task<String> ISpotifyPlaylistsApi.ReorderPlaylistItemsAsync(
            String id,
            Int32 rangeStart,
            Int32 insertBefore,
            Int32? rangeLength,
            String? snapshotId,
            CancellationToken cancellationToken)
        {
            return this.ReorderPlaylistItemsAsync(id, rangeStart, insertBefore, rangeLength, snapshotId, null, cancellationToken);
        }

        Task ISpotifyPlaylistsApi.ReplacePlaylistItemsAsync(String id, IEnumerable<String> uris, CancellationToken cancellationToken)
        {
            return this.ReplacePlaylistItemsAsync(id, uris, null, cancellationToken);
        }

        Task<String> ISpotifyPlaylistsApi.AddItemsToPlaylistAsync(
            String id,
            IEnumerable<String> uris,
            Int32? position,
            CancellationToken cancellationToken)
        {
            return this.AddItemsToPlaylistAsync(id, uris, position, null, cancellationToken);
        }

        Task<String> ISpotifyPlaylistsApi.RemoveItemsFromPlaylistAsync(
            String id,
            IEnumerable<String> uris,
            String? snapshotId,
            CancellationToken cancellationToken)
        {
            return this.RemoveItemsFromPlaylistAsync(id, uris, snapshotId, null, cancellationToken);
        }
        #endregion
    }
}