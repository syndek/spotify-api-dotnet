using System;
using System.Collections.Generic;
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
            var uriBuilder = new SpotifyUriBuilder($"{SpotifyApiClient.BaseUri}/me/following/contains")
                .AppendToQuery("type", "artist")
                .AppendJoinToQuery("ids", ',', ids);

            return base.SendAsync<IReadOnlyList<Boolean>>(
                uriBuilder.Build(),
                HttpMethod.Get,
                accessTokenProvider,
                cancellationToken);
        }

        /// <inheritdoc/>
        public Task<IReadOnlyList<Boolean>> CheckCurrentUserFollowsUsersAsync(
            IEnumerable<String> ids,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            var uriBuilder = new SpotifyUriBuilder($"{SpotifyApiClient.BaseUri}/me/following/contains")
                .AppendToQuery("type", "user")
                .AppendJoinToQuery("ids", ',', ids);

            return base.SendAsync<IReadOnlyList<Boolean>>(
                uriBuilder.Build(),
                HttpMethod.Get,
                accessTokenProvider,
                cancellationToken);
        }

        /// <inheritdoc/>
        public Task<IReadOnlyList<Boolean>> CheckUsersFollowPlaylistAsync(
            String playlistId,
            IEnumerable<String> ids,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            var uriBuilder = new SpotifyUriBuilder($"{SpotifyApiClient.BaseUri}/playlists/{playlistId}/followers/contains")
                .AppendJoinToQuery("ids", ',', ids);

            return base.SendAsync<IReadOnlyList<Boolean>>(
                uriBuilder.Build(),
                HttpMethod.Get,
                accessTokenProvider,
                cancellationToken);
        }

        /// <inheritdoc/>
        public Task FollowArtistsAsync(
            IEnumerable<String> ids,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            var uriBuilder = new SpotifyUriBuilder($"{SpotifyApiClient.BaseUri}/me/following")
                .AppendToQuery("type", "artist");

            var content = new StringContent(JsonSerializer.Serialize(new { ids }), Encoding.UTF8, MediaTypeNames.Application.Json);

            return base.SendAsync(
                uriBuilder.Build(),
                HttpMethod.Put,
                content,
                accessTokenProvider,
                cancellationToken);
        }

        /// <inheritdoc/>
        public Task FollowUsersAsync(
            IEnumerable<String> ids,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
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
                new($"{SpotifyApiClient.BaseUri}/playlists/{id}/followers"),
                HttpMethod.Delete,
                content: null,
                accessTokenProvider,
                cancellationToken);
        }
    }
}