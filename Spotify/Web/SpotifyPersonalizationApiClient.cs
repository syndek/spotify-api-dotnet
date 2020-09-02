using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Spotify.ObjectModel;
using Spotify.ObjectModel.EnumConverters;
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

        /// <inheritdoc/>
        public Task<Paging<Artist>> GetTopArtistsForCurrentUserAsync(
            Int32? limit = null,
            Int32? offset = null,
            TimeRange? timeRange = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            var uriBuilder = new SpotifyUriBuilder($"{SpotifyApiClient.BaseUri}/me/top/artists")
                .AppendToQueryIfNotNull("limit", limit)
                .AppendToQueryIfNotNull("offset", offset)
                .AppendToQueryIfNotNull("time_range", timeRange?.ToSpotifyString());

            return base.SendAsync<Paging<Artist>>(
                uriBuilder.Build(),
                HttpMethod.Get,
                accessTokenProvider,
                cancellationToken);
        }

        /// <inheritdoc/>
        public Task<Paging<Track>> GetTopTracksForCurrentUserAsync(
            Int32? limit = null,
            Int32? offset = null,
            TimeRange? timeRange = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            var uriBuilder = new SpotifyUriBuilder($"{SpotifyApiClient.BaseUri}/me/top/tracks")
                .AppendToQueryIfNotNull("limit", limit)
                .AppendToQueryIfNotNull("offset", offset)
                .AppendToQueryIfNotNull("time_range", timeRange?.ToSpotifyString());

            return base.SendAsync<Paging<Track>>(
                uriBuilder.Build(),
                HttpMethod.Get,
                accessTokenProvider,
                cancellationToken);
        }
    }
}