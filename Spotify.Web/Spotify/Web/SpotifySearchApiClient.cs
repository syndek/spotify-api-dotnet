using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Spotify.ObjectModel;
using Spotify.ObjectModel.Serialization.EnumConverters;
using Spotify.Web.Authorization;

namespace Spotify.Web
{
    /// <summary>
    /// Represents a <see cref="SpotifyApiClient"/> for retrieving Spotify catalog information
    /// about albums, artists, playlists, tracks, shows or episodes that match a keyword string.
    /// </summary>
    public class SpotifySearchApiClient : SpotifyApiClient, ISpotifySearchApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpotifySearchApiClient"/> class with the specified <paramref name="httpClient"/>.
        /// </summary>
        /// <param name="httpClient">An instance of <see cref="HttpClient"/> to use for requests to the Spotify Web API.</param>
        public SpotifySearchApiClient(HttpClient httpClient) : base(httpClient) { }

        /// <inheritdoc/>
        public Task<SearchResult> SearchAsync(
            String query,
            SearchResultTypes types,
            CountryCode? market = null,
            Int32? limit = null,
            Int32? offset = null,
            Boolean? includeExternal = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            var uriBuilder = new SpotifyUriBuilder($"{SpotifyApiClient.BaseUrl}/search")
                .AppendToQuery("query", query.Replace(" ", "%20"))
                .AppendJoinToQuery("type", ',', types.GetFlags().Select(value => value.GetName().ToLower()))
                .AppendToQueryIfNotNull("market", market?.ToSpotifyString())
                .AppendToQueryIfNotNull("limit", limit)
                .AppendToQueryIfNotNull("offset", offset)
                .AppendToQueryIfNotNull("include_external", includeExternal);

            return base.SendAsync<SearchResult>(
                uriBuilder.Build(),
                HttpMethod.Get,
                content: null,
                accessTokenProvider,
                cancellationToken);
        }

        Task<SearchResult> ISpotifySearchApi.SearchAsync(
            String query,
            SearchResultTypes types,
            CountryCode? market,
            Int32? limit,
            Int32? offset,
            Boolean? includeExternal,
            CancellationToken cancellationToken)
        {
            return this.SearchAsync(query, types, market, limit, offset, includeExternal, null, cancellationToken);
        }
    }
}