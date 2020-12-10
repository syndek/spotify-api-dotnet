using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Spotify.ObjectModel;
using Spotify.Web.Authorization;

namespace Spotify.Web
{
    /// <summary>
    /// Represents a <see cref="SpotifyApiClient"/> for retrieving information about one or more episodes from the Spotify catalog.
    /// </summary>
    public class SpotifyEpisodesApiClient : SpotifyApiClient, ISpotifyEpisodesApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpotifyEpisodesApiClient"/> class with the specified <paramref name="httpClient"/>.
        /// </summary>
        /// <param name="httpClient">An instance of <see cref="HttpClient"/> to use for requests to the Spotify Web API.</param>
        public SpotifyEpisodesApiClient(HttpClient httpClient) : base(httpClient) { }

        /// <summary>
        /// Asynchronously get multiple <see cref="Episode"/> objects from the Spotify catalog.
        /// </summary>
        /// <param name="ids">
        /// An <see cref="IEnumerable{T}"/> of <see cref="string"/> objects
        /// representing the Spotify IDs of the <see cref="Episode"/> objects to get.
        /// </param>
        /// <param name="market">An optional <see cref="CountryCode"/> to limit results to a certain market only.</param>
        /// <param name="accessTokenProvider">An optional <see cref="IAccessTokenProvider"/> to use instead of the default.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.</returns>
        public Task<Episode> GetEpisodeAsync(
            string id,
            CountryCode? market = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            var uriBuilder = new SpotifyUriBuilder($"{SpotifyApiClient.BaseUrl}/episodes/{id}")
                .AppendToQueryIfNotNull("market", market);

            return base.SendAsync<Episode>(
                uriBuilder.Build(),
                HttpMethod.Get,
                content: null,
                accessTokenProvider,
                cancellationToken);
        }

        /// <summary>
        /// Asynchronously get an <see cref="Episode"/> from the Spotify catalog.
        /// </summary>
        /// <param name="id">A <see cref="string"/> representing the Spotify ID of the <see cref="Episode"/> to get.</param>
        /// <param name="market">An optional <see cref="CountryCode"/> to limit results to a certain market only.</param>
        /// <param name="accessTokenProvider">An optional <see cref="IAccessTokenProvider"/> to use instead of the default.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.</returns>
        public Task<IReadOnlyList<Episode>> GetEpisodesAsync(
            IEnumerable<string> ids,
            CountryCode? market = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            var uriBuilder = new SpotifyUriBuilder($"{SpotifyApiClient.BaseUrl}/episodes")
                .AppendJoinToQuery("ids", ',', ids)
                .AppendToQueryIfNotNull("market", market);

            return base.SendAsync<IReadOnlyList<Episode>>(
                uriBuilder.Build(),
                HttpMethod.Get,
                content: null,
                accessTokenProvider,
                cancellationToken);
        }

        #region ISpotifyEpisodesApi Implementation
        Task<Episode> ISpotifyEpisodesApi.GetEpisodeAsync(string id, CountryCode? market, CancellationToken cancellationToken)
        {
            return this.GetEpisodeAsync(id, market, null, cancellationToken);
        }

        Task<IReadOnlyList<Episode>> ISpotifyEpisodesApi.GetEpisodesAsync(IEnumerable<string> ids, CountryCode? market, CancellationToken cancellationToken)
        {
            return this.GetEpisodesAsync(ids, market, null, cancellationToken);
        }
        #endregion
    }
}