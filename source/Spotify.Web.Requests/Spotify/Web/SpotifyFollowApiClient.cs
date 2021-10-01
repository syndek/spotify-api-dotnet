using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Spotify.Web.Authorization;

namespace Spotify.Web
{
    /// <summary>
    /// Represents a <see cref="SpotifyApiClient"/> for managing the artists, users and playlists that a Spotify user follows.
    /// </summary>
    public class SpotifyFollowApiClient : SpotifyApiClient, ISpotifyFollowApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpotifyFollowApiClient"/> class with the specified <paramref name="httpClient"/>.
        /// </summary>
        /// <param name="httpClient">An instance of <see cref="HttpClient"/> to use for requests to the Spotify Web API.</param>
        public SpotifyFollowApiClient(HttpClient httpClient) : base(httpClient) { }

        public Task<IReadOnlyList<bool>> CheckCurrentUserFollowsArtistsAsync(
            IEnumerable<string> ids,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            var uriBuilder = new SpotifyUriBuilder($"{BaseUrl}/me/following/contains")
                .AppendToQuery("type", "artist")
                .AppendJoinToQuery("ids", ',', ids);

            return SendAsync<IReadOnlyList<bool>>(
                uriBuilder.Build(),
                HttpMethod.Get,
                content: null,
                accessTokenProvider,
                cancellationToken);
        }

        public Task<IReadOnlyList<bool>> CheckCurrentUserFollowsUsersAsync(
            IEnumerable<string> ids,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            var uriBuilder = new SpotifyUriBuilder($"{BaseUrl}/me/following/contains")
                .AppendToQuery("type", "user")
                .AppendJoinToQuery("ids", ',', ids);

            return SendAsync<IReadOnlyList<bool>>(
                uriBuilder.Build(),
                HttpMethod.Get,
                content: null,
                accessTokenProvider,
                cancellationToken);
        }

        public Task<IReadOnlyList<bool>> CheckUsersFollowPlaylistAsync(
            string id,
            IEnumerable<string> userIds,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            var uriBuilder = new SpotifyUriBuilder($"{BaseUrl}/playlists/{id}/followers/contains")
                .AppendJoinToQuery("ids", ',', userIds);

            return SendAsync<IReadOnlyList<bool>>(
                uriBuilder.Build(),
                HttpMethod.Get,
                content: null,
                accessTokenProvider,
                cancellationToken);
        }

        public Task FollowArtistsAsync(
            IEnumerable<string> ids,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            return EditFollowing("artist", HttpMethod.Put, ids, accessTokenProvider, cancellationToken);
        }

        public Task FollowUsersAsync(
            IEnumerable<string> ids,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            return EditFollowing("user", HttpMethod.Put, ids, accessTokenProvider, cancellationToken);
        }

        public Task FollowPlaylistAsync(
            string id,
            bool? publicFollow = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            var content = new StringContent(
                @"{""public"":" + (publicFollow ?? true).ToString().ToLower() + "}",
                Encoding.UTF8,
                MediaTypeNames.Application.Json);

            return SendAsync(
                new($"{BaseUrl}/playlists/{id}/followers"),
                HttpMethod.Put,
                content,
                accessTokenProvider,
                cancellationToken);
        }

        public Task UnfollowArtistsAsync(
            IEnumerable<string> ids,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            return EditFollowing("artist", HttpMethod.Delete, ids, accessTokenProvider, cancellationToken);
        }

        public Task UnfollowUsersAsync(
            IEnumerable<string> ids,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            return EditFollowing("user", HttpMethod.Delete, ids, accessTokenProvider, cancellationToken);
        }

        public Task UnfollowPlaylistAsync(
            string id,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            return SendAsync(
                new($"{BaseUrl}/playlists/{id}/followers"),
                HttpMethod.Delete,
                content: null,
                accessTokenProvider,
                cancellationToken);
        }

        private Task EditFollowing(
            string type,
            HttpMethod method,
            IEnumerable<string> ids,
            IAccessTokenProvider? accessTokenProvider,
            CancellationToken cancellationToken)
        {
            var uriBuilder = new SpotifyUriBuilder($"{BaseUrl}/me/following")
                .AppendToQuery("type", type)
                .AppendJoinToQuery("ids", ',', ids);

            return SendAsync(
                uriBuilder.Build(),
                method,
                content: null,
                accessTokenProvider,
                cancellationToken);
        }

        #region ISpotifyFollowApi Implementation
        Task<IReadOnlyList<bool>> ISpotifyFollowApi.CheckCurrentUserFollowsArtistsAsync(
            IEnumerable<string> ids,
            CancellationToken cancellationToken)
        {
            return CheckCurrentUserFollowsArtistsAsync(ids, null, cancellationToken);
        }

        Task<IReadOnlyList<bool>> ISpotifyFollowApi.CheckCurrentUserFollowsUsersAsync(
            IEnumerable<string> ids,
            CancellationToken cancellationToken)
        {
            return CheckCurrentUserFollowsUsersAsync(ids, null, cancellationToken);
        }

        Task<IReadOnlyList<bool>> ISpotifyFollowApi.CheckUsersFollowPlaylistAsync(
            string id,
            IEnumerable<string> userIds,
            CancellationToken cancellationToken)
        {
            return CheckUsersFollowPlaylistAsync(id, userIds, null, cancellationToken);
        }

        Task ISpotifyFollowApi.FollowArtistsAsync(IEnumerable<string> ids, CancellationToken cancellationToken)
        {
            return FollowArtistsAsync(ids, null, cancellationToken);
        }

        Task ISpotifyFollowApi.FollowUsersAsync(IEnumerable<string> ids, CancellationToken cancellationToken)
        {
            return FollowUsersAsync(ids, null, cancellationToken);
        }

        Task ISpotifyFollowApi.FollowPlaylistAsync(string id, bool? publicFollow, CancellationToken cancellationToken)
        {
            return FollowPlaylistAsync(id, publicFollow, null, cancellationToken);
        }

        Task ISpotifyFollowApi.UnfollowArtistsAsync(IEnumerable<string> ids, CancellationToken cancellationToken)
        {
            return UnfollowArtistsAsync(ids, null, cancellationToken);
        }

        Task ISpotifyFollowApi.UnfollowUsersAsync(IEnumerable<string> ids, CancellationToken cancellationToken)
        {
            return UnfollowUsersAsync(ids, null, cancellationToken);
        }

        Task ISpotifyFollowApi.UnfollowPlaylistAsync(string id, CancellationToken cancellationToken)
        {
            return UnfollowPlaylistAsync(id, null, cancellationToken);
        }
        #endregion
    }
}
