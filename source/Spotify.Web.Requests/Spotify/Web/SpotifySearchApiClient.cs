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

        /// <summary>
        /// Asynchronously search the Spotify catalog using a keyword string.
        /// </summary>
        /// <param name="query">
        /// A <see cref="string"/> representing the keywords to search with.
        /// This can contain optional field filters and operators. Information about how to use these can be found in
        /// the <see href="https://spotify.dev/documentation/web-api/reference/search/search/">Spotify Developer documentation</see>.
        /// </param>
        /// <param name="types">The <see cref="SearchResultTypes"/> to include in the search.</param>
        /// <param name="market">
        /// An optional <see cref="CountryCode"/> to limit returned content to a certain market.
        /// Playlists are not affected by this parameter.
        /// <see cref="CountryCode.FromToken"/> can be used if the access token used is a valid user access token.
        /// </param>
        /// <param name="limit">
        /// The maximum number of results to return.
        /// This limit is applied within each type, not on the total response.
        /// </param>
        /// <param name="offset">The index of the first result to return.</param>
        /// <param name="includeExternal">Whether or not to include any relevant audio content that is hosted externally.</param>
        /// <param name="accessTokenProvider">An optional <see cref="IAccessTokenProvider"/> to use instead of the default.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.</returns>
        public Task<SearchResult> SearchAsync(
            string query,
            SearchResultTypes types,
            CountryCode? market = null,
            int? limit = null,
            int? offset = null,
            bool? includeExternal = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            var uriBuilder = new SpotifyUriBuilder($"{BaseUrl}/search")
                .AppendToQuery("query", query.Replace(" ", "%20"))
                .AppendJoinToQuery("type", ',', types.GetFlags().Select(value => value.GetName().ToLower()))
                .AppendToQueryIfNotNull("market", market?.ToSpotifyString())
                .AppendToQueryIfNotNull("limit", limit)
                .AppendToQueryIfNotNull("offset", offset)
                .AppendToQueryIfNotNull("include_external", includeExternal);

            return SendAsync<SearchResult>(
                uriBuilder.Build(),
                HttpMethod.Get,
                content: null,
                accessTokenProvider,
                cancellationToken);
        }

        #region ISpotifySearchApi Implementation
        Task<SearchResult> ISpotifySearchApi.SearchAsync(
            string query,
            SearchResultTypes types,
            CountryCode? market,
            int? limit,
            int? offset,
            bool? includeExternal,
            CancellationToken cancellationToken)
        {
            return SearchAsync(query, types, market, limit, offset, includeExternal, null, cancellationToken);
        }
        #endregion
    }
}
