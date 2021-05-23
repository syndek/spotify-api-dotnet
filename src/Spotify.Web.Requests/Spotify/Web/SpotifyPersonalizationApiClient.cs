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
    /// Represents a <see cref="SpotifyApiClient"/> for accessing a Spotify user's personalization data.
    /// </summary>
    public class SpotifyPersonalizationApiClient : SpotifyApiClient, ISpotifyPersonalizationApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpotifyPersonalizationApiClient"/> class with the specified <paramref name="httpClient"/>.
        /// </summary>
        /// <param name="httpClient">An instance of <see cref="HttpClient"/> to use for requests to the Spotify Web API.</param>
        public SpotifyPersonalizationApiClient(HttpClient httpClient) : base(httpClient) { }

        public Task<Paging<Artist>> GetTopArtistsForCurrentUserAsync(
            int? limit = null,
            int? offset = null,
            TimeRange? timeRange = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            var uriBuilder = new SpotifyUriBuilder($"{BaseUrl}/me/top/artists")
                .AppendToQueryIfNotNull("limit", limit)
                .AppendToQueryIfNotNull("offset", offset)
                .AppendToQueryIfNotNull("time_range", timeRange?.ToSpotifyString());

            return SendAsync<Paging<Artist>>(
                uriBuilder.Build(),
                HttpMethod.Get,
                content: null,
                accessTokenProvider,
                cancellationToken);
        }

        public Task<Paging<Track>> GetTopTracksForCurrentUserAsync(
            int? limit = null,
            int? offset = null,
            TimeRange? timeRange = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            var uriBuilder = new SpotifyUriBuilder($"{BaseUrl}/me/top/tracks")
                .AppendToQueryIfNotNull("limit", limit)
                .AppendToQueryIfNotNull("offset", offset)
                .AppendToQueryIfNotNull("time_range", timeRange?.ToSpotifyString());

            return SendAsync<Paging<Track>>(
                uriBuilder.Build(),
                HttpMethod.Get,
                content: null,
                accessTokenProvider,
                cancellationToken);
        }

        #region ISpotifyPersonalizationApi Implementation
        Task<Paging<Artist>> ISpotifyPersonalizationApi.GetTopArtistsForCurrentUserAsync(
            int? limit,
            int? offset,
            TimeRange? timeRange,
            CancellationToken cancellationToken)
        {
            return GetTopArtistsForCurrentUserAsync(limit, offset, timeRange, null, cancellationToken);
        }

        Task<Paging<Track>> ISpotifyPersonalizationApi.GetTopTracksForCurrentUserAsync(
            int? limit,
            int? offset,
            TimeRange? timeRange,
            CancellationToken cancellationToken)
        {
            return GetTopTracksForCurrentUserAsync(limit, offset, timeRange, null, cancellationToken);
        }
        #endregion
    }
}
