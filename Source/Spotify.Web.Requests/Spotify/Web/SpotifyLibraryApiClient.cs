using Spotify.ObjectModel;
using Spotify.ObjectModel.Collections;
using Spotify.ObjectModel.Serialization.EnumConverters;
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
            IEnumerable<string> ids,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            return this.SaveAsync("album", ids, accessTokenProvider, cancellationToken);
        }

        public Task RemoveAlbumsAsync(
            IEnumerable<string> ids,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            return this.RemoveAsync("album", ids, accessTokenProvider, cancellationToken);
        }

        public Task<Paging<Saved<Album>>> GetSavedAlbumsAsync(
            int? limit = null,
            int? offset = null,
            CountryCode? market = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            return this.GetAsync<Saved<Album>>("album", limit, offset, market, accessTokenProvider, cancellationToken);
        }

        public Task<IReadOnlyList<bool>> CheckSavedAlbumsAsync(
            IEnumerable<string> ids,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            return this.CheckAsync("album", ids, accessTokenProvider, cancellationToken);
        }

        public Task SaveTracksAsync(
            IEnumerable<string> ids,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            return this.SaveAsync("track", ids, accessTokenProvider, cancellationToken);
        }

        public Task RemoveTracksAsync(
            IEnumerable<string> ids,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            return this.RemoveAsync("track", ids, accessTokenProvider, cancellationToken);
        }

        public Task<Paging<Saved<Track>>> GetSavedTracksAsync(
            int? limit = null,
            int? offset = null,
            CountryCode? market = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            return this.GetAsync<Saved<Track>>("track", limit, offset, market, accessTokenProvider, cancellationToken);
        }

        public Task<IReadOnlyList<bool>> CheckSavedTracksAsync(
            IEnumerable<string> ids,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            return this.CheckAsync("track", ids, accessTokenProvider, cancellationToken);
        }

        public Task SaveShowsAsync(
            IEnumerable<string> ids,
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
            IEnumerable<string> ids,
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
            int? limit = null,
            int? offset = null,
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

        public Task<IReadOnlyList<bool>> CheckSavedShowsAsync(
            IEnumerable<string> ids,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            return this.CheckAsync("show", ids, accessTokenProvider, cancellationToken);
        }

        private Task SaveAsync(
            string objectType,
            IEnumerable<string> ids,
            IAccessTokenProvider? accessTokenProvider,
            CancellationToken cancellationToken)
        {
            return base.SendAsync(
                new($"{SpotifyApiClient.BaseUrl}/me/{objectType}s"),
                HttpMethod.Put,
                new StringContent($"[{string.Join(',', ids)}]", Encoding.UTF8, MediaTypeNames.Application.Json),
                accessTokenProvider,
                cancellationToken);
        }

        private Task RemoveAsync(
            string objectType,
            IEnumerable<string> ids,
            IAccessTokenProvider? accessTokenProvider,
            CancellationToken cancellationToken)
        {
            return base.SendAsync(
                new($"{SpotifyApiClient.BaseUrl}/me/{objectType}s"),
                HttpMethod.Delete,
                new StringContent($"[{string.Join(',', ids)}]", Encoding.UTF8, MediaTypeNames.Application.Json),
                accessTokenProvider,
                cancellationToken);
        }

        private Task<Paging<TObject>> GetAsync<TObject>(
            string objectType,
            int? limit,
            int? offset,
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

        private Task<IReadOnlyList<bool>> CheckAsync(
            string objectType,
            IEnumerable<string> ids,
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
        Task ISpotifyLibraryApi.SaveAlbumsAsync(IEnumerable<string> ids, CancellationToken cancellationToken)
        {
            return this.SaveAlbumsAsync(ids, null, cancellationToken);
        }

        Task ISpotifyLibraryApi.RemoveAlbumsAsync(IEnumerable<string> ids, CancellationToken cancellationToken)
        {
            return this.RemoveAlbumsAsync(ids, null, cancellationToken);
        }

        Task<Paging<Saved<Album>>> ISpotifyLibraryApi.GetSavedAlbumsAsync(int? limit, int? offset, CountryCode? market, CancellationToken cancellationToken)
        {
            return this.GetSavedAlbumsAsync(limit, offset, market, null, cancellationToken);
        }

        Task<IReadOnlyList<bool>> ISpotifyLibraryApi.CheckSavedAlbumsAsync(IEnumerable<string> ids, CancellationToken cancellationToken)
        {
            return this.CheckSavedAlbumsAsync(ids, null, cancellationToken);
        }

        Task ISpotifyLibraryApi.SaveTracksAsync(IEnumerable<string> ids, CancellationToken cancellationToken)
        {
            return this.SaveTracksAsync(ids, null, cancellationToken);
        }

        Task ISpotifyLibraryApi.RemoveTracksAsync(IEnumerable<string> ids, CancellationToken cancellationToken)
        {
            return this.RemoveTracksAsync(ids, null, cancellationToken);
        }

        Task<Paging<Saved<Track>>> ISpotifyLibraryApi.GetSavedTracksAsync(int? limit, int? offset, CountryCode? market, CancellationToken cancellationToken)
        {
            return this.GetSavedTracksAsync(limit, offset, market, null, cancellationToken);
        }

        Task<IReadOnlyList<bool>> ISpotifyLibraryApi.CheckSavedTracksAsync(IEnumerable<string> ids, CancellationToken cancellationToken)
        {
            return this.CheckSavedTracksAsync(ids, null, cancellationToken);
        }

        Task ISpotifyLibraryApi.SaveShowsAsync(IEnumerable<string> ids, CancellationToken cancellationToken)
        {
            return this.SaveShowsAsync(ids, null, cancellationToken);
        }

        Task ISpotifyLibraryApi.RemoveShowsAsync(IEnumerable<string> ids, CountryCode? market, CancellationToken cancellationToken)
        {
            return this.RemoveShowsAsync(ids, market, null, cancellationToken);
        }

        Task<Paging<Saved<Show>>> ISpotifyLibraryApi.GetSavedShowsAsync(int? limit, int? offset, CancellationToken cancellationToken)
        {
            return this.GetSavedShowsAsync(limit, offset, null, cancellationToken);
        }

        Task<IReadOnlyList<bool>> ISpotifyLibraryApi.CheckSavedShowsAsync(IEnumerable<string> ids, CancellationToken cancellationToken)
        {
            return this.CheckSavedShowsAsync(ids, null, cancellationToken);
        }
        #endregion
    }
}