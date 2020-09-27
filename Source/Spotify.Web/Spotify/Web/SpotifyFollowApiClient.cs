using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
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

        /// <inheritdoc/>
        public Task<IReadOnlyList<Boolean>> CheckCurrentUserFollowsArtistsAsync(
            IEnumerable<String> ids,
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

        /// <inheritdoc/>
        public Task<IReadOnlyList<Boolean>> CheckCurrentUserFollowsUsersAsync(
            IEnumerable<String> ids,
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

        /// <inheritdoc/>
        public Task<IReadOnlyList<Boolean>> CheckUsersFollowPlaylistAsync(
            String id,
            IEnumerable<String> userIds,
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

        /// <inheritdoc/>
        public Task FollowArtistsAsync(
            IEnumerable<String> ids,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            return this.Follow("artist", ids, accessTokenProvider, cancellationToken);
        }

        /// <inheritdoc/>
        public Task FollowUsersAsync(
            IEnumerable<String> ids,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            return this.Follow("user", ids, accessTokenProvider, cancellationToken);
        }

        /// <inheritdoc/>
        public Task FollowPlaylistAsync(
            String id,
            Boolean? publicFollow = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task UnfollowArtistsAsync(
            IEnumerable<String> ids,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task UnfollowUsersAsync(
            IEnumerable<String> ids,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task UnfollowPlaylistAsync(
            String id,
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

        private Task Follow(
            String type,
            IEnumerable<String> ids,
            IAccessTokenProvider? accessTokenProvider,
            CancellationToken cancellationToken)
        {
            var uriBuilder = new SpotifyUriBuilder($"{SpotifyApiClient.BaseUrl}/me/following")
                .AppendToQuery("type", type);

            var content = new StringContent(
                "{ids:[" + String.Join(',', ids.Select(id => '"' + id + '"')) + "]}",
                Encoding.UTF8,
                MediaTypeNames.Application.Json);

            return base.SendAsync(
                uriBuilder.Build(),
                HttpMethod.Put,
                content,
                accessTokenProvider,
                cancellationToken);
        }

        #region ISpotifyFollowApi Implementation
        Task<IReadOnlyList<Boolean>> ISpotifyFollowApi.CheckCurrentUserFollowsArtistsAsync(
            IEnumerable<String> ids,
            CancellationToken cancellationToken)
        {
            return this.CheckCurrentUserFollowsArtistsAsync(ids, null, cancellationToken);
        }

        Task<IReadOnlyList<Boolean>> ISpotifyFollowApi.CheckCurrentUserFollowsUsersAsync(
            IEnumerable<String> ids,
            CancellationToken cancellationToken)
        {
            return this.CheckCurrentUserFollowsUsersAsync(ids, null, cancellationToken);
        }

        Task<IReadOnlyList<Boolean>> ISpotifyFollowApi.CheckUsersFollowPlaylistAsync(
            String id,
            IEnumerable<String> userIds,
            CancellationToken cancellationToken)
        {
            return this.CheckUsersFollowPlaylistAsync(id, userIds, null, cancellationToken);
        }

        Task ISpotifyFollowApi.FollowArtistsAsync(IEnumerable<String> ids, CancellationToken cancellationToken)
        {
            return this.FollowArtistsAsync(ids, null, cancellationToken);
        }

        Task ISpotifyFollowApi.FollowUsersAsync(IEnumerable<String> ids, CancellationToken cancellationToken)
        {
            return this.FollowUsersAsync(ids, null, cancellationToken);
        }

        Task ISpotifyFollowApi.FollowPlaylistAsync(String id, Boolean? publicFollow, CancellationToken cancellationToken)
        {
            return this.FollowPlaylistAsync(id, publicFollow, null, cancellationToken);
        }

        Task ISpotifyFollowApi.UnfollowArtistsAsync(IEnumerable<String> ids, CancellationToken cancellationToken)
        {
            return this.UnfollowArtistsAsync(ids, null, cancellationToken);
        }

        Task ISpotifyFollowApi.UnfollowUsersAsync(IEnumerable<String> ids, CancellationToken cancellationToken)
        {
            return this.UnfollowUsersAsync(ids, null, cancellationToken);
        }

        Task ISpotifyFollowApi.UnfollowPlaylistAsync(String id, CancellationToken cancellationToken)
        {
            return this.UnfollowPlaylistAsync(id, null, cancellationToken);
        }
        #endregion
    }
}