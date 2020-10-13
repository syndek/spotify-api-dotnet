using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Spotify.ObjectModel;
using Spotify.ObjectModel.Collections;
using Spotify.ObjectModel.Serialization.EnumConverters;
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

        public Task SaveAlbumsAsync(
            IEnumerable<String> ids,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            return this.SaveAsync("album", ids, accessTokenProvider, cancellationToken);
        }

        public Task RemoveAlbumsAsync(
            IEnumerable<String> ids,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            return this.RemoveAsync("album", ids, accessTokenProvider, cancellationToken);
        }

        public Task<Paging<Saved<Album>>> GetSavedAlbumsAsync(
            Int32? limit = null,
            Int32? offset = null,
            CountryCode? market = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            return this.GetAsync<Saved<Album>>("album", limit, offset, market, accessTokenProvider, cancellationToken);
        }

        public Task<IReadOnlyList<Boolean>> CheckSavedAlbumsAsync(
            IEnumerable<String> ids,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            return this.CheckAsync("album", ids, accessTokenProvider, cancellationToken);
        }

        public Task SaveTracksAsync(
            IEnumerable<String> ids,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            return this.SaveAsync("track", ids, accessTokenProvider, cancellationToken);
        }

        public Task RemoveTracksAsync(
            IEnumerable<String> ids,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            return this.RemoveAsync("track", ids, accessTokenProvider, cancellationToken);
        }

        public Task<Paging<Saved<Track>>> GetSavedTracksAsync(
            Int32? limit = null,
            Int32? offset = null,
            CountryCode? market = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            return this.GetAsync<Saved<Track>>("track", limit, offset, market, accessTokenProvider, cancellationToken);
        }

        public Task<IReadOnlyList<Boolean>> CheckSavedTracksAsync(
            IEnumerable<String> ids,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            return this.CheckAsync("track", ids, accessTokenProvider, cancellationToken);
        }

        public Task SaveShowsAsync(
            IEnumerable<String> ids,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            var uriBuilder = new SpotifyUriBuilder($"{SpotifyApiClient.BaseUrl}/me/shows")
                .AppendJoinToQuery("ids", ',', ids);

            return base.SendAsync(
                uriBuilder.Build(),
                HttpMethod.Put,
                content: null,
                accessTokenProvider,
                cancellationToken);
        }

        public Task RemoveShowsAsync(
            IEnumerable<String> ids,
            CountryCode? market = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            var uriBuilder = new SpotifyUriBuilder($"{SpotifyApiClient.BaseUrl}/me/shows")
                .AppendJoinToQuery("ids", ',', ids)
                .AppendToQueryIfNotNull("market", market?.ToSpotifyString());

            return base.SendAsync(
                uriBuilder.Build(),
                HttpMethod.Delete,
                content: null,
                accessTokenProvider,
                cancellationToken);
        }

        public Task<Paging<Saved<Show>>> GetSavedShowsAsync(
            Int32? limit = null,
            Int32? offset = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            var uriBuilder = new SpotifyUriBuilder($"{SpotifyApiClient.BaseUrl}/me/shows")
                .AppendToQueryIfNotNull("limit", limit)
                .AppendToQueryIfNotNull("offset", offset);

            return base.SendAsync<Paging<Saved<Show>>>(
                uriBuilder.Build(),
                HttpMethod.Get,
                content: null,
                accessTokenProvider,
                cancellationToken);
        }

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
            return base.SendAsync(
                new($"{SpotifyApiClient.BaseUrl}/me/{objectType}s"),
                HttpMethod.Put,
                new StringContent($"[{String.Join(',', ids)}]", Encoding.UTF8, MediaTypeNames.Application.Json),
                accessTokenProvider,
                cancellationToken);
        }

        private Task RemoveAsync(
            String objectType,
            IEnumerable<String> ids,
            IAccessTokenProvider? accessTokenProvider,
            CancellationToken cancellationToken)
        {
            return base.SendAsync(
                new($"{SpotifyApiClient.BaseUrl}/me/{objectType}s"),
                HttpMethod.Delete,
                new StringContent($"[{String.Join(',', ids)}]", Encoding.UTF8, MediaTypeNames.Application.Json),
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
            var uriBuilder = new SpotifyUriBuilder($"{SpotifyApiClient.BaseUrl}/me/{objectType}s")
                .AppendToQueryIfNotNull("limit", limit)
                .AppendToQueryIfNotNull("offset", offset)
                .AppendToQueryIfNotNull("market", market?.ToSpotifyString());

            return base.SendAsync<Paging<TObject>>(
                uriBuilder.Build(),
                HttpMethod.Get,
                content: null,
                accessTokenProvider,
                cancellationToken);
        }

        private Task<IReadOnlyList<Boolean>> CheckAsync(
            String objectType,
            IEnumerable<String> ids,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            var uriBuilder = new SpotifyUriBuilder($"{SpotifyApiClient.BaseUrl}/me/{objectType}s/contains")
                .AppendJoinToQuery("ids", ',', ids);

            return base.SendAsync<IReadOnlyList<Boolean>>(
                uriBuilder.Build(),
                HttpMethod.Get,
                content: null,
                accessTokenProvider,
                cancellationToken);
        }

        #region ISpotifyLibraryApi Implementation
        Task ISpotifyLibraryApi.SaveAlbumsAsync(IEnumerable<String> ids, CancellationToken cancellationToken)
        {
            return this.SaveAlbumsAsync(ids, null, cancellationToken);
        }

        Task ISpotifyLibraryApi.RemoveAlbumsAsync(IEnumerable<String> ids, CancellationToken cancellationToken)
        {
            return this.RemoveAlbumsAsync(ids, null, cancellationToken);
        }

        Task<Paging<Saved<Album>>> ISpotifyLibraryApi.GetSavedAlbumsAsync(Int32? limit, Int32? offset, CountryCode? market, CancellationToken cancellationToken)
        {
            return this.GetSavedAlbumsAsync(limit, offset, market, null, cancellationToken);
        }

        Task<IReadOnlyList<Boolean>> ISpotifyLibraryApi.CheckSavedAlbumsAsync(IEnumerable<String> ids, CancellationToken cancellationToken)
        {
            return this.CheckSavedAlbumsAsync(ids, null, cancellationToken);
        }

        Task ISpotifyLibraryApi.SaveTracksAsync(IEnumerable<String> ids, CancellationToken cancellationToken)
        {
            return this.SaveTracksAsync(ids, null, cancellationToken);
        }

        Task ISpotifyLibraryApi.RemoveTracksAsync(IEnumerable<String> ids, CancellationToken cancellationToken)
        {
            return this.RemoveTracksAsync(ids, null, cancellationToken);
        }

        Task<Paging<Saved<Track>>> ISpotifyLibraryApi.GetSavedTracksAsync(Int32? limit, Int32? offset, CountryCode? market, CancellationToken cancellationToken)
        {
            return this.GetSavedTracksAsync(limit, offset, market, null, cancellationToken);
        }

        Task<IReadOnlyList<Boolean>> ISpotifyLibraryApi.CheckSavedTracksAsync(IEnumerable<String> ids, CancellationToken cancellationToken)
        {
            return this.CheckSavedTracksAsync(ids, null, cancellationToken);
        }

        Task ISpotifyLibraryApi.SaveShowsAsync(IEnumerable<String> ids, CancellationToken cancellationToken)
        {
            return this.SaveShowsAsync(ids, null, cancellationToken);
        }

        Task ISpotifyLibraryApi.RemoveShowsAsync(IEnumerable<String> ids, CountryCode? market, CancellationToken cancellationToken)
        {
            return this.RemoveShowsAsync(ids, market, null, cancellationToken);
        }

        Task<Paging<Saved<Show>>> ISpotifyLibraryApi.GetSavedShowsAsync(Int32? limit, Int32? offset, CancellationToken cancellationToken)
        {
            return this.GetSavedShowsAsync(limit, offset, null, cancellationToken);
        }

        Task<IReadOnlyList<Boolean>> ISpotifyLibraryApi.CheckSavedShowsAsync(IEnumerable<String> ids, CancellationToken cancellationToken)
        {
            return this.CheckSavedShowsAsync(ids, null, cancellationToken);
        }
        #endregion
    }
}