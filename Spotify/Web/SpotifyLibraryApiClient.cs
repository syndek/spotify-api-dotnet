using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Spotify.ObjectModel;
using Spotify.ObjectModel.EnumConverters;
using Spotify.Web.Authorization;

namespace Spotify.Web
{
    /// <summary>
    /// Represents a <see cref="SpotifyApiClient"/> for managing the current user's Spotify library.
    /// </summary>
    public class SpotifyLibraryApiClient : SpotifyApiClient, ISpotifyLibraryApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpotifyLibraryApiClient"/> class with the specified <paramref name="httpClient"/>.
        /// </summary>
        /// <param name="httpClient">An instance of <see cref="HttpClient"/> to use for requests to the Spotify Web API.</param>
        public SpotifyLibraryApiClient(HttpClient httpClient) : base(httpClient) { }

        /// <inheritdoc/>
        public Task SaveAlbumsAsync(
            IEnumerable<String> ids,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            return this.SaveAsync("album", ids, accessTokenProvider, cancellationToken);
        }

        /// <inheritdoc/>
        public Task RemoveAlbumsAsync(
            IEnumerable<String> ids,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            return this.RemoveAsync("album", ids, accessTokenProvider, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<Paging<Saved<Album>>> GetSavedAlbumsAsync(
            Int32? limit = null,
            Int32? offset = null,
            CountryCode? market = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            return this.GetAsync<Saved<Album>>(
                "album",
                limit,
                offset,
                market,
                accessTokenProvider,
                cancellationToken);
        }

        /// <inheritdoc/>
        public Task<IReadOnlyList<Boolean>> CheckSavedAlbumsAsync(
            IEnumerable<String> ids,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            return this.CheckAsync("album", ids, accessTokenProvider, cancellationToken);
        }

        /// <inheritdoc/>
        public Task SaveTracksAsync(
            IEnumerable<String> ids,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            return this.SaveAsync("track", ids, accessTokenProvider, cancellationToken);
        }

        /// <inheritdoc/>
        public Task RemoveTracksAsync(
            IEnumerable<String> ids,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            return this.RemoveAsync("track", ids, accessTokenProvider, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<Paging<Saved<Track>>> GetSavedTracksAsync(
            Int32? limit = null,
            Int32? offset = null,
            CountryCode? market = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            return this.GetAsync<Saved<Track>>(
                "track",
                limit,
                offset,
                market,
                accessTokenProvider,
                cancellationToken);
        }

        /// <inheritdoc/>
        public Task<IReadOnlyList<Boolean>> CheckSavedTracksAsync(
            IEnumerable<String> ids,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            return this.CheckAsync("track", ids, accessTokenProvider, cancellationToken);
        }

        /// <inheritdoc/>
        public Task SaveShowsAsync(
            IEnumerable<String> ids,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            var uriBuilder = new SpotifyUriBuilder($"{SpotifyApiClient.BaseUri}/me/shows")
                .AppendJoinToQuery("ids", ',', ids);

            return base.SendAsync(
                uriBuilder.Build(),
                HttpMethod.Put,
                null,
                accessTokenProvider,
                cancellationToken);
        }

        /// <inheritdoc/>
        public Task RemoveShowsAsync(
            IEnumerable<String> ids,
            CountryCode? market = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            var uriBuilder = new SpotifyUriBuilder($"{SpotifyApiClient.BaseUri}/me/shows")
                .AppendJoinToQuery("ids", ',', ids)
                .AppendToQueryIfNotNull("market", market?.ToSpotifyString());

            return base.SendAsync(
                uriBuilder.Build(),
                HttpMethod.Delete,
                null,
                accessTokenProvider,
                cancellationToken);
        }

        /// <inheritdoc/>
        public Task<Paging<Saved<Show>>> GetSavedShowsAsync(
            Int32? limit = null,
            Int32? offset = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            var uriBuilder = new SpotifyUriBuilder($"{SpotifyApiClient.BaseUri}/me/shows")
                .AppendToQueryIfNotNull("limit", limit)
                .AppendToQueryIfNotNull("offset", offset);

            return base.SendAsync<Paging<Saved<Show>>>(
                uriBuilder.Build(),
                HttpMethod.Get,
                accessTokenProvider,
                cancellationToken);
        }

        /// <inheritdoc/>
        public Task<IReadOnlyList<Boolean>> CheckSavedShowsAsync(
            IEnumerable<String> ids,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            return this.CheckAsync("show", ids, accessTokenProvider, cancellationToken);
        }

        private Task SaveAsync(
            String objectType,
            IEnumerable<String> ids,
            IAccessTokenProvider? accessTokenProvider,
            CancellationToken cancellationToken)
        {
            var content = new StringContent($"[{String.Join(',', ids)}]", Encoding.UTF8, MediaTypeNames.Application.Json);

            return base.SendAsync(
                new($"{SpotifyApiClient.BaseUri}/me/{objectType}s"),
                HttpMethod.Put,
                content,
                accessTokenProvider,
                cancellationToken);
        }

        private Task RemoveAsync(
            String objectType,
            IEnumerable<String> ids,
            IAccessTokenProvider? accessTokenProvider,
            CancellationToken cancellationToken)
        {
            var content = new StringContent($"[{String.Join(',', ids)}]", Encoding.UTF8, MediaTypeNames.Application.Json);

            return base.SendAsync(
                new($"{SpotifyApiClient.BaseUri}/me/{objectType}s"),
                HttpMethod.Delete,
                content,
                accessTokenProvider,
                cancellationToken);
        }

        private Task<Paging<TObject>> GetAsync<TObject>(
            String objectType,
            Int32? limit,
            Int32? offset,
            CountryCode? market,
            IAccessTokenProvider? accessTokenProvider,
            CancellationToken cancellationToken)
        {
            var uriBuilder = new SpotifyUriBuilder($"{SpotifyApiClient.BaseUri}/me/{objectType}s")
                .AppendToQueryIfNotNull("limit", limit)
                .AppendToQueryIfNotNull("offset", offset)
                .AppendToQueryIfNotNull("market", market?.ToSpotifyString());

            return base.SendAsync<Paging<TObject>>(
                uriBuilder.Build(),
                HttpMethod.Get,
                accessTokenProvider,
                cancellationToken);
        }

        private Task<IReadOnlyList<Boolean>> CheckAsync(
            String objectType,
            IEnumerable<String> ids,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            var uriBuilder = new SpotifyUriBuilder($"{SpotifyApiClient.BaseUri}/me/{objectType}s/contains")
                .AppendJoinToQuery("ids", ',', ids);

            return base.SendAsync<IReadOnlyList<Boolean>>(
                uriBuilder.Build(),
                HttpMethod.Get,
                accessTokenProvider,
                cancellationToken);
        }
    }
}