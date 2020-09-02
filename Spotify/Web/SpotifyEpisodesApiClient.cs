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

        /// <inheritdoc/>
        public Task<Episode> GetEpisodeAsync(
            String id,
            CountryCode? market = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            var uriBuilder = new SpotifyUriBuilder($"{SpotifyApiClient.BaseUri}/episodes/{id}")
                .AppendToQueryIfNotNull("market", market);

            return base.SendAsync<Episode>(
                uriBuilder.Build(),
                HttpMethod.Get,
                accessTokenProvider,
                cancellationToken);
        }

        /// <inheritdoc/>
        public Task<IReadOnlyList<Episode>> GetEpisodesAsync(
            IEnumerable<String> ids,
            CountryCode? market = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            var uriBuilder = new SpotifyUriBuilder($"{SpotifyApiClient.BaseUri}/episodes")
                .AppendJoinToQuery("ids", ',', ids)
                .AppendToQueryIfNotNull("market", market);

            return base.SendAsync<IReadOnlyList<Episode>>(
                uriBuilder.Build(),
                HttpMethod.Get,
                accessTokenProvider,
                cancellationToken);
        }
    }
}