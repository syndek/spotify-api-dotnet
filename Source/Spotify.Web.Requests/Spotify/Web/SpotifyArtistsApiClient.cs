using Spotify.ObjectModel;
using Spotify.ObjectModel.Collections;
using Spotify.ObjectModel.Serialization.EnumConverters;
using Spotify.Web.Authorization;
using Spotify.Web.RequestObjects;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Spotify.Web
{
    /// <summary>
    /// Represents a <see cref="SpotifyApiClient"/> for retrieving information about one or more artists from the Spotify catalog.
    /// </summary>
    public class SpotifyArtistsApiClient : SpotifyApiClient, ISpotifyArtistsApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpotifyArtistsApiClient"/> class with the specified <paramref name="httpClient"/>.
        /// </summary>
        /// <param name="httpClient">An instance of <see cref="HttpClient"/> to use for requests to the Spotify Web API.</param>
        public SpotifyArtistsApiClient(HttpClient httpClient) : base(httpClient) { }

        /// <summary>
        /// Asynchronously get multiple <see cref="Artist"/> objects from the Spotify catalog.
        /// </summary>
        /// <param name="ids">
        /// An <see cref="IEnumerable{T}"/> of <see cref="string"/> objects
        /// representing the Spotify IDs of the <see cref="Artist"/> objects to get.
        /// </param>
        /// <param name="accessTokenProvider">An optional <see cref="IAccessTokenProvider"/> to use instead of the default.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.</returns>
        public Task<IReadOnlyList<Artist>> GetArtistsAsync(
            IEnumerable<string> ids,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            var uriBuilder = new SpotifyUriBuilder($"{BaseUrl}/artists")
                .AppendJoinToQuery("ids", ',', ids);

            return SendAsync<IReadOnlyList<Artist>>(
                uriBuilder.Build(),
                HttpMethod.Get,
                content: null,
                accessTokenProvider,
                cancellationToken);
        }

        /// <summary>
        /// Asynchronously get an <see cref="Artist"/> from the Spotify catalog.
        /// </summary>
        /// <param name="id">A <see cref="string"/> representing the Spotify ID of the <see cref="Artist"/> to get.</param>
        /// <param name="accessTokenProvider">An optional <see cref="IAccessTokenProvider"/> to use instead of the default.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.</returns>
        public Task<Artist> GetArtistAsync(
            string id,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            return SendAsync<Artist>(
                new($"{BaseUrl}/artists/{id}"),
                HttpMethod.Get,
                content: null,
                accessTokenProvider,
                cancellationToken);
        }

        /// <summary>
        /// Asynchronously get the albums of an <see cref="Artist"/> from the Spotify catalog.
        /// </summary>
        /// <param name="id">A <see cref="string"/> representing the Spotify ID of the <see cref="Artist"/> to get the albums of.</param>
        /// <param name="includeGroups">The <see cref="AlbumGroups"/> that the result should be filtered to include.</param>
        /// <param name="market">An optional <see cref="CountryCode"/> to limit the result to one particular geographical market.</param>
        /// <param name="limit">The maximum number of results to return.</param>
        /// <param name="offset">The index of the first result to return.</param>
        /// <param name="accessTokenProvider">An optional <see cref="IAccessTokenProvider"/> to use instead of the default.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.</returns>
        public Task<Paging<SimplifiedAlbum>> GetArtistAlbumsAsync(
            string id,
            AlbumGroups? includeGroups = null,
            CountryCode? market = null,
            int? limit = null,
            int? offset = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            var uriBuilder = new SpotifyUriBuilder($"{BaseUrl}/artists/{id}/albums")
                .AppendJoinToQueryIfNotNull("include_groups", ',', includeGroups?.ToSpotifyStrings())
                .AppendToQueryIfNotNull("market", market?.ToSpotifyString())
                .AppendToQueryIfNotNull("limit", limit)
                .AppendToQueryIfNotNull("offset", offset);

            return SendAsync<Paging<SimplifiedAlbum>>(
                uriBuilder.Build(),
                HttpMethod.Get,
                content: null,
                accessTokenProvider,
                cancellationToken);
        }

        /// <summary>
        /// Asynchronously get the top tracks of an <see cref="Artist"/> from the Spotify catalog.
        /// </summary>
        /// <param name="id">
        /// A <see cref="string"/> representing the Spotify ID of the <see cref="Artist"/> to get the top tracks of.
        /// </param>
        /// <param name="market">A <see cref="CountryCode"/> representing the market to get the top tracks for.</param>
        /// <param name="accessTokenProvider">An optional <see cref="IAccessTokenProvider"/> to use instead of the default.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.</returns>
        public Task<IReadOnlyList<Track>> GetArtistTopTracksAsync(
            string id,
            CountryCode market,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            var uriBuilder = new SpotifyUriBuilder($"{BaseUrl}/artists/{id}/top-tracks")
                .AppendToQuery("country", market);

            return SendAsync<IReadOnlyList<Track>>(
                uriBuilder.Build(),
                HttpMethod.Get,
                content: null,
                accessTokenProvider,
                cancellationToken);
        }

        /// <summary>
        /// Asynchronously get the related artists of an <see cref="Artist"/> from the Spotify catalog.
        /// </summary>
        /// <param name="id">
        /// A <see cref="string"/> representing the Spotify ID of the <see cref="Artist"/> to get the related artists of.
        /// </param>
        /// <param name="accessTokenProvider">An optional <see cref="IAccessTokenProvider"/> to use instead of the default.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.</returns>
        public async Task<IReadOnlyList<Artist>> GetArtistRelatedArtistsAsync(
            string id,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            return await SendAsync<NamedArray<Artist>>(
                new($"{BaseUrl}/artists/{id}/related-artists"),
                HttpMethod.Get,
                content: null,
                accessTokenProvider,
                cancellationToken);
        }

        #region ISpotifyArtistsApi Implementation
        Task<IReadOnlyList<Artist>> ISpotifyArtistsApi.GetArtistsAsync(
            IEnumerable<string> ids,
            CancellationToken cancellationToken)
        {
            return GetArtistsAsync(ids, null, cancellationToken);
        }

        Task<Artist> ISpotifyArtistsApi.GetArtistAsync(string id, CancellationToken cancellationToken)
        {
            return GetArtistAsync(id, null, cancellationToken);
        }

        Task<Paging<SimplifiedAlbum>> ISpotifyArtistsApi.GetArtistAlbumsAsync(
            string id,
            AlbumGroups? includeGroups,
            CountryCode? market,
            int? limit,
            int? offset,
            CancellationToken cancellationToken)
        {
            return GetArtistAlbumsAsync(id, includeGroups, market, limit, offset, null, cancellationToken);
        }

        Task<IReadOnlyList<Track>> ISpotifyArtistsApi.GetArtistTopTracksAsync(
            string id,
            CountryCode market,
            CancellationToken cancellationToken)
        {
            return GetArtistTopTracksAsync(id, market, null, cancellationToken);
        }

        Task<IReadOnlyList<Artist>> ISpotifyArtistsApi.GetArtistRelatedArtistsAsync(
            string id,
            CancellationToken cancellationToken)
        {
            return GetArtistRelatedArtistsAsync(id, null, cancellationToken);
        }
        #endregion
    }
}
