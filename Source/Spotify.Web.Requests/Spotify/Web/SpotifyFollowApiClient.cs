using Spotify.Web.Authorization;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
            var uriBuilder = new SpotifyUriBuilder($"{SpotifyApiClient.BaseUrl}/me/following/contains")
                .AppendToQuery("type", "artist")
                .AppendJoinToQuery("ids", ',', ids);

            return base.SendAsync<IReadOnlyList<Boolean>>(
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
            var uriBuilder = new SpotifyUriBuilder($"{SpotifyApiClient.BaseUrl}/me/following/contains")
                .AppendToQuery("type", "user")
                .AppendJoinToQuery("ids", ',', ids);

            return base.SendAsync<IReadOnlyList<Boolean>>(
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
            var uriBuilder = new SpotifyUriBuilder($"{SpotifyApiClient.BaseUrl}/playlists/{id}/followers/contains")
                .AppendJoinToQuery("ids", ',', userIds);

            return base.SendAsync<IReadOnlyList<Boolean>>(
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
            return this.EditFollowing("artist", HttpMethod.Put, ids, accessTokenProvider, cancellationToken);
        }

        public Task FollowUsersAsync(
            IEnumerable<string> ids,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            return this.EditFollowing("user", HttpMethod.Put, ids, accessTokenProvider, cancellationToken);
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

            return base.SendAsync(
                new Uri($"{SpotifyApiClient.BaseUrl}/playlists/{id}/followers"),
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
            return this.EditFollowing("artist", HttpMethod.Delete, ids, accessTokenProvider, cancellationToken);
        }

        public Task UnfollowUsersAsync(
            IEnumerable<string> ids,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            return this.EditFollowing("user", HttpMethod.Delete, ids, accessTokenProvider, cancellationToken);
        }

        public Task UnfollowPlaylistAsync(
            string id,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            return base.SendAsync(
                new($"{SpotifyApiClient.BaseUrl}/playlists/{id}/followers"),
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
            var uriBuilder = new SpotifyUriBuilder($"{SpotifyApiClient.BaseUrl}/me/following")
                .AppendToQuery("type", type)
                .AppendJoinToQuery("ids", ',', ids);

            return base.SendAsync(
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
            return this.CheckCurrentUserFollowsArtistsAsync(ids, null, cancellationToken);
        }

        Task<IReadOnlyList<bool>> ISpotifyFollowApi.CheckCurrentUserFollowsUsersAsync(
            IEnumerable<string> ids,
            CancellationToken cancellationToken)
        {
            return this.CheckCurrentUserFollowsUsersAsync(ids, null, cancellationToken);
        }

        Task<IReadOnlyList<bool>> ISpotifyFollowApi.CheckUsersFollowPlaylistAsync(
            string id,
            IEnumerable<string> userIds,
            CancellationToken cancellationToken)
        {
            return this.CheckUsersFollowPlaylistAsync(id, userIds, null, cancellationToken);
        }

        Task ISpotifyFollowApi.FollowArtistsAsync(IEnumerable<string> ids, CancellationToken cancellationToken)
        {
            return this.FollowArtistsAsync(ids, null, cancellationToken);
        }

        Task ISpotifyFollowApi.FollowUsersAsync(IEnumerable<string> ids, CancellationToken cancellationToken)
        {
            return this.FollowUsersAsync(ids, null, cancellationToken);
        }

        Task ISpotifyFollowApi.FollowPlaylistAsync(string id, bool? publicFollow, CancellationToken cancellationToken)
        {
            return this.FollowPlaylistAsync(id, publicFollow, null, cancellationToken);
        }

        Task ISpotifyFollowApi.UnfollowArtistsAsync(IEnumerable<string> ids, CancellationToken cancellationToken)
        {
            return this.UnfollowArtistsAsync(ids, null, cancellationToken);
        }

        Task ISpotifyFollowApi.UnfollowUsersAsync(IEnumerable<string> ids, CancellationToken cancellationToken)
        {
            return this.UnfollowUsersAsync(ids, null, cancellationToken);
        }

        Task ISpotifyFollowApi.UnfollowPlaylistAsync(string id, CancellationToken cancellationToken)
        {
            return this.UnfollowPlaylistAsync(id, null, cancellationToken);
        }
        #endregion
    }
}