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
    /// Represents a <see cref="SpotifyApiClient"/> for retrieving information about one or more shows from the Spotify catalog.
    /// </summary>
    public class SpotifyShowsApiClient : SpotifyApiClient, ISpotifyShowsApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpotifyShowsApiClient"/> class with the specified <paramref name="httpClient"/>.
        /// </summary>
        /// <param name="httpClient">An instance of <see cref="HttpClient"/> to use for requests to the Spotify Web API.</param>
        public SpotifyShowsApiClient(HttpClient httpClient) : base(httpClient) { }

        public Task<IReadOnlyList<SimplifiedShow>> GetShowsAsync(
            IEnumerable<String> ids,
            CountryCode? market = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            var uriBuilder = new SpotifyUriBuilder($"{SpotifyApiClient.BaseUrl}/shows")
                .AppendJoinToQuery("ids", ',', ids)
                .AppendToQueryIfNotNull("market", market?.ToSpotifyString());

            return base.SendAsync<IReadOnlyList<SimplifiedShow>>(
                uriBuilder.Build(),
                HttpMethod.Get,
                content: null,
                accessTokenProvider,
                cancellationToken);
        }

        public Task<Show> GetShowAsync(
            String id,
            CountryCode? market = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            var uriBuilder = new SpotifyUriBuilder($"{SpotifyApiClient.BaseUrl}/shows/{id}")
                .AppendToQueryIfNotNull("market", market?.ToSpotifyString());

            return base.SendAsync<Show>(
                uriBuilder.Build(),
                HttpMethod.Get,
                content: null,
                accessTokenProvider,
                cancellationToken);
        }

        public Task<Paging<SimplifiedEpisode>> GetShowEpisodesAsync(
            String id,
            Int32? limit = null,
            Int32? offset = null,
            CountryCode? market = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            var uriBuilder = new SpotifyUriBuilder($"{SpotifyApiClient.BaseUrl}/shows/{id}")
                .AppendToQueryIfNotNull("limit", limit)
                .AppendToQueryIfNotNull("offset", offset)
                .AppendToQueryIfNotNull("market", market?.ToSpotifyString());

            return base.SendAsync<Paging<SimplifiedEpisode>>(
                uriBuilder.Build(),
                HttpMethod.Get,
                content: null,
                accessTokenProvider,
                cancellationToken);
        }

        #region ISpotifyShowsApi Implementation
        Task<IReadOnlyList<SimplifiedShow>> ISpotifyShowsApi.GetShowsAsync(
            IEnumerable<String> ids,
            CountryCode? market,
            CancellationToken cancellationToken)
        {
            return this.GetShowsAsync(ids, market, null, cancellationToken);
        }

        Task<Show> ISpotifyShowsApi.GetShowAsync(String id, CountryCode? market, CancellationToken cancellationToken)
        {
            return this.GetShowAsync(id, market, null, cancellationToken);
        }

        Task<Paging<SimplifiedEpisode>> ISpotifyShowsApi.GetShowEpisodesAsync(
            String id,
            Int32? limit,
            Int32? offset,
            CountryCode? market,
            CancellationToken cancellationToken)
        {
            return this.GetShowEpisodesAsync(id, limit, offset, market, null, cancellationToken);
        }
        #endregion
    }
}