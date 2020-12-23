using Spotify.ObjectModel;
using Spotify.ObjectModel.Collections;
using Spotify.Web.Authorization;
using Spotify.Web.RequestObjects;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

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
            string userId,
            string name,
            string? description = null,
            bool? isPublic = null,
            bool? isCollaborative = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            return SendAsync<Playlist>(
                new($"{BaseUrl}/users/{userId}/playlists"),
                HttpMethod.Post,
                new StringContent(
                    JsonSerializer.Serialize(
                        new PlaylistDetails(name, description, isPublic, isCollaborative),
                        RequestObjectSerializerOptions),
                    Encoding.UTF8,
                    MediaTypeNames.Application.Json),
                accessTokenProvider,
                cancellationToken);
        }

        public Task<Playlist> GetPlaylistAsync(
            string id,
            CountryCode? market = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            var uriBuilder = new SpotifyUriBuilder($"{BaseUrl}/playlists/{id}")
                .AppendToQueryIfNotNull("market", market)
                .AppendToQuery("additional_types", "episode");

            return SendAsync<Playlist>(
                uriBuilder.Build(),
                HttpMethod.Get,
                content: null,
                accessTokenProvider,
                cancellationToken);
        }

        public Task<Paging<IPlayable>> GetPlaylistItemsAsync(
            string id,
            int? limit = null,
            int? offset = null,
            CountryCode? market = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            var uriBuilder = new SpotifyUriBuilder($"{BaseUrl}/playlists/{id}")
                .AppendToQueryIfNotNull("limit", limit)
                .AppendToQueryIfNotNull("offset", offset)
                .AppendToQueryIfNotNull("market", market)
                .AppendToQuery("additional_types", "episode");

            return SendAsync<Paging<IPlayable>>(
                uriBuilder.Build(),
                HttpMethod.Get,
                content: null,
                accessTokenProvider,
                cancellationToken);
        }

        public Task<Paging<SimplifiedPlaylist>> GetCurrentUserPlaylistsAsync(
            int? limit = null,
            int? offset = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            var uriBuilder = new SpotifyUriBuilder($"{BaseUrl}/me/playlists")
                .AppendToQueryIfNotNull("limit", limit)
                .AppendToQueryIfNotNull("offset", offset);

            return SendAsync<Paging<SimplifiedPlaylist>>(
                uriBuilder.Build(),
                HttpMethod.Get,
                content: null,
                accessTokenProvider,
                cancellationToken);
        }

        public Task<Paging<SimplifiedPlaylist>> GetUserPlaylistsAsync(
            string userId,
            int? limit = null,
            int? offset = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            var uriBuilder = new SpotifyUriBuilder($"{BaseUrl}/users/{userId}/playlists")
                .AppendToQueryIfNotNull("limit", limit)
                .AppendToQueryIfNotNull("offset", offset);

            return SendAsync<Paging<SimplifiedPlaylist>>(
                uriBuilder.Build(),
                HttpMethod.Get,
                content: null,
                accessTokenProvider,
                cancellationToken);
        }

        public Task ChangePlaylistDetailsAsync(
            string id,
            string? name = null,
            string? description = null,
            bool? isPublic = null,
            bool? isCollaborative = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            return SendAsync(
                new($"{BaseUrl}/playlists/{id}"),
                HttpMethod.Put,
                new StringContent(
                    JsonSerializer.Serialize(
                        new PlaylistDetails(name, description, isPublic, isCollaborative),
                        RequestObjectSerializerOptions),
                    Encoding.UTF8,
                    MediaTypeNames.Application.Json),
                accessTokenProvider,
                cancellationToken);
        }

        public Task<IReadOnlyList<Image>> GetPlaylistCoverImageAsync(
            string id,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            return SendAsync<IReadOnlyList<Image>>(
                new($"{BaseUrl}/playlists/{id}/images"),
                HttpMethod.Get,
                content: null,
                accessTokenProvider,
                cancellationToken);
        }

        public Task SetPlaylistCoverImageAsync(
            string id,
            string base64Image,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            return SendAsync<IReadOnlyList<Image>>(
                new($"{BaseUrl}/playlists/{id}/images"),
                HttpMethod.Put,
                new StringContent(base64Image, Encoding.UTF8, MediaTypeNames.Image.Jpeg),
                accessTokenProvider,
                cancellationToken);
        }

        public Task<string> ReorderPlaylistItemsAsync(
            string id,
            int rangeStart,
            int insertBefore,
            int? rangeLength = null,
            string? snapshotId = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            // TODO: Find a way to make this deserialize a SnapshotId object.
            return SendAsync<string>(
                new($"{BaseUrl}/playlists/{id}/tracks"),
                HttpMethod.Put,
                new StringContent(
                    JsonSerializer.Serialize(
                        new ReorderPlaylistItemsParameters(rangeStart, insertBefore, rangeLength, snapshotId),
                        RequestObjectSerializerOptions),
                    Encoding.UTF8,
                    MediaTypeNames.Application.Json),
                accessTokenProvider,
                cancellationToken);
        }

        public Task ReplacePlaylistItemsAsync(
            string id,
            IEnumerable<string> uris,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            var uriBuilder = new SpotifyUriBuilder($"{BaseUrl}/playlists/{id}/tracks")
                .AppendJoinToQuery("uris", ',', uris);

            return SendAsync(
                uriBuilder.Build(),
                HttpMethod.Put,
                content: null,
                accessTokenProvider,
                cancellationToken);
        }

        public Task<string> AddItemsToPlaylistAsync(
            string id,
            IEnumerable<string> uris,
            int? position = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            var uriBuilder = new SpotifyUriBuilder($"{BaseUrl}/playlists/{id}")
                .AppendJoinToQuery("uris", ',', uris)
                .AppendToQueryIfNotNull("position", position);

            // TODO: Find a way to make this deserialize a SnapshotId object.
            return SendAsync<string>(
                uriBuilder.Build(),
                HttpMethod.Post,
                content: null,
                accessTokenProvider,
                cancellationToken);
        }

        public Task<string> RemoveItemsFromPlaylistAsync(
            string id,
            IEnumerable<string> uris,
            string? snapshotId = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            // TODO: Implement with respect to https://spotify.dev/documentation/web-api/reference/playlists/remove-tracks-playlist.
            throw new NotImplementedException();
        }

        #region ISpotifyPlaylistsApi Implementation
        Task<Playlist> ISpotifyPlaylistsApi.CreatePlaylistAsync(
            string userId,
            string name,
            string? description,
            bool? isPublic,
            bool? isCollaborative,
            CancellationToken cancellationToken)
        {
            return CreatePlaylistAsync(userId, name, description, isPublic, isCollaborative, null, cancellationToken);
        }

        Task<Playlist> ISpotifyPlaylistsApi.GetPlaylistAsync(string id, CountryCode? market, CancellationToken cancellationToken)
        {
            return GetPlaylistAsync(id, market, null, cancellationToken);
        }

        Task<Paging<IPlayable>> ISpotifyPlaylistsApi.GetPlaylistItemsAsync(
            string id,
            int? limit,
            int? offset,
            CountryCode? market,
            CancellationToken cancellationToken)
        {
            return GetPlaylistItemsAsync(id, limit, offset, market, null, cancellationToken);
        }

        Task<Paging<SimplifiedPlaylist>> ISpotifyPlaylistsApi.GetCurrentUserPlaylistsAsync(
            int? limit,
            int? offset,
            CancellationToken cancellationToken)
        {
            return GetCurrentUserPlaylistsAsync(limit, offset, null, cancellationToken);
        }

        Task<Paging<SimplifiedPlaylist>> ISpotifyPlaylistsApi.GetUserPlaylistsAsync(
            string userId,
            int? limit,
            int? offset,
            CancellationToken cancellationToken)
        {
            return GetUserPlaylistsAsync(userId, limit, offset, null, cancellationToken);
        }

        Task ISpotifyPlaylistsApi.ChangePlaylistDetailsAsync(
            string id,
            string? name,
            string? description,
            bool? isPublic,
            bool? isCollaborative,
            CancellationToken cancellationToken)
        {
            return ChangePlaylistDetailsAsync(id, name, description, isPublic, isCollaborative, null, cancellationToken);
        }

        Task<IReadOnlyList<Image>> ISpotifyPlaylistsApi.GetPlaylistCoverImageAsync(string id, CancellationToken cancellationToken)
        {
            return GetPlaylistCoverImageAsync(id, null, cancellationToken);
        }

        Task ISpotifyPlaylistsApi.SetPlaylistCoverImageAsync(string id, string base64Image, CancellationToken cancellationToken)
        {
            return SetPlaylistCoverImageAsync(id, base64Image, null, cancellationToken);
        }

        Task<string> ISpotifyPlaylistsApi.ReorderPlaylistItemsAsync(
            string id,
            int rangeStart,
            int insertBefore,
            int? rangeLength,
            string? snapshotId,
            CancellationToken cancellationToken)
        {
            return ReorderPlaylistItemsAsync(id, rangeStart, insertBefore, rangeLength, snapshotId, null, cancellationToken);
        }

        Task ISpotifyPlaylistsApi.ReplacePlaylistItemsAsync(string id, IEnumerable<string> uris, CancellationToken cancellationToken)
        {
            return ReplacePlaylistItemsAsync(id, uris, null, cancellationToken);
        }

        Task<string> ISpotifyPlaylistsApi.AddItemsToPlaylistAsync(
            string id,
            IEnumerable<string> uris,
            int? position,
            CancellationToken cancellationToken)
        {
            return AddItemsToPlaylistAsync(id, uris, position, null, cancellationToken);
        }

        Task<string> ISpotifyPlaylistsApi.RemoveItemsFromPlaylistAsync(
            string id,
            IEnumerable<string> uris,
            string? snapshotId,
            CancellationToken cancellationToken)
        {
            return RemoveItemsFromPlaylistAsync(id, uris, snapshotId, null, cancellationToken);
        }
        #endregion
    }
}
