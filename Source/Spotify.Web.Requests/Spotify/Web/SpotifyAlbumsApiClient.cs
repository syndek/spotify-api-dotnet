using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Spotify.ObjectModel;
using Spotify.ObjectModel.Collections;
using Spotify.ObjectModel.Serialization.EnumConverters;
using Spotify.Web.Authorization;

namespace Spotify.Web
{
    /// <summary>
    /// Represents a <see cref="SpotifyApiClient"/> for retrieving information about one or more albums from the Spotify catalog.
    /// </summary>
    public class SpotifyAlbumsApiClient : SpotifyApiClient, ISpotifyAlbumsApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpotifyAlbumsApiClient"/> class with the specified <paramref name="httpClient"/>.
        /// </summary>
        /// <param name="httpClient">An instance of <see cref="HttpClient"/> to use for requests to the Spotify Web API.</param>
        public SpotifyAlbumsApiClient(HttpClient httpClient) : base(httpClient) { }

        /// <summary>
        /// Asynchronously get multiple <see cref="Album"/> objects from the Spotify catalog.
        /// </summary>
        /// <param name="ids">
        /// An <see cref="IEnumerable{T}"/> of <see cref="string"/> objects
        /// representing the Spotify IDs of the <see cref="Album"/> objects to get.
        /// </param>
        /// <param name="market">
        /// An optional <see cref="CountryCode"/> to apply
        /// <see href="https://spotify.dev/documentation/general/guides/track-relinking-guide/">track relinking</see>.
        /// </param>
        /// <param name="accessTokenProvider">An optional <see cref="IAccessTokenProvider"/> to use instead of the default.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.</returns>
        public Task<IReadOnlyList<Album?>> GetAlbumsAsync(
            IEnumerable<string> ids,
            CountryCode? market = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            var uriBuilder = new SpotifyUriBuilder($"{SpotifyApiClient.BaseUrl}/albums")
                .AppendJoinToQuery("ids", ',', ids)
                .AppendToQueryIfNotNull("market", market?.ToSpotifyString());

            return base.SendAsync<IReadOnlyList<Album?>>(
                uriBuilder.Build(),
                HttpMethod.Get,
                content: null,
                accessTokenProvider,
                cancellationToken);
        }

        /// <summary>
        /// Asynchronously get an <see cref="Album"/> from the Spotify catalog.
        /// </summary>
        /// <param name="id">A <see cref="string"/> representing the Spotify ID of the <see cref="Album"/> to get.</param>
        /// <param name="market">
        /// An optional <see cref="CountryCode"/> to apply
        /// <see href="https://spotify.dev/documentation/general/guides/track-relinking-guide/">track relinking</see>.
        /// </param>
        /// <param name="accessTokenProvider">An optional <see cref="IAccessTokenProvider"/> to use instead of the default.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.</returns>
        public Task<Album> GetAlbumAsync(
            string id,
            CountryCode? market = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            var uriBuilder = new SpotifyUriBuilder($"{SpotifyApiClient.BaseUrl}/albums/{id}")
                .AppendToQueryIfNotNull("market", market?.ToSpotifyString());

            return base.SendAsync<Album>(
                uriBuilder.Build(),
                HttpMethod.Get,
                content: null,
                accessTokenProvider,
                cancellationToken);
        }

        /// <summary>
        /// Asynchronously get the tracks of an <see cref="Album"/> from the Spotify catalog.
        /// </summary>
        /// <param name="id">A <see cref="string"/> representing the Spotify ID of the <see cref="Album"/> to get the tracks of.</param>
        /// <param name="limit">The maximum number of results to return.</param>
        /// <param name="offset">The index of the first result to return.</param>
        /// <param name="market">
        /// An optional <see cref="CountryCode"/> to apply
        /// <see href="https://spotify.dev/documentation/general/guides/track-relinking-guide/">track relinking</see>.
        /// </param>
        /// <param name="accessTokenProvider">An optional <see cref="IAccessTokenProvider"/> to use instead of the default.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.</returns>
        public Task<Paging<SimplifiedTrack>> GetAlbumTracksAsync(
            string id,
            int? limit = null,
            int? offset = null,
            CountryCode? market = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            var uriBuilder = new SpotifyUriBuilder($"{SpotifyApiClient.BaseUrl}/albums/{id}/tracks")
                .AppendToQueryIfNotNull("limit", limit)
                .AppendToQueryIfNotNull("offset", offset)
                .AppendToQueryIfNotNull("market", market?.ToSpotifyString());

            return base.SendAsync<Paging<SimplifiedTrack>>(
                uriBuilder.Build(),
                HttpMethod.Get,
                content: null,
                accessTokenProvider,
                cancellationToken);
        }

        #region ISpotifyAlbumsApi Implementation
        Task<IReadOnlyList<Album?>> ISpotifyAlbumsApi.GetAlbumsAsync(
            IEnumerable<string> ids,
            CountryCode? market,
            CancellationToken cancellationToken)
        {
            return this.GetAlbumsAsync(ids, market, null, cancellationToken);
        }

        Task<Album> ISpotifyAlbumsApi.GetAlbumAsync(string id, CountryCode? market, CancellationToken cancellationToken)
        {
            return this.GetAlbumAsync(id, market, null, cancellationToken);
        }

        Task<Paging<SimplifiedTrack>> ISpotifyAlbumsApi.GetAlbumTracksAsync(
            string id,
            int? limit,
            int? offset,
            CountryCode? market,
            CancellationToken cancellationToken)
        {
            return this.GetAlbumTracksAsync(id, limit, offset, market, null, cancellationToken);
        }
        #endregion
    }
}