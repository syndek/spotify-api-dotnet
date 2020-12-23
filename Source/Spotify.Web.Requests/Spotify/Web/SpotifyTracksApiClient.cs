using Spotify.ObjectModel;
using Spotify.Web.Authorization;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Spotify.Web
{
    /// <summary>
    /// Represents a <see cref="SpotifyApiClient"/> for retrieving information about one or more tracks from the Spotify catalog.
    /// </summary>
    public class SpotifyTracksApiClient : SpotifyApiClient, ISpotifyTracksApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpotifyTracksApiClient"/> class with the specified <paramref name="httpClient"/>.
        /// </summary>
        /// <param name="httpClient">An instance of <see cref="HttpClient"/> to use for requests to the Spotify Web API.</param>
        public SpotifyTracksApiClient(HttpClient httpClient) : base(httpClient) { }

        public Task<IReadOnlyList<Track>> GetTracksAsync(
            IReadOnlyList<string> ids,
            CountryCode? market = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            var uriBuilder = new SpotifyUriBuilder($"{BaseUrl}/tracks")
                .AppendJoinToQuery("ids", ',', ids)
                .AppendToQueryIfNotNull("market", market);

            return SendAsync<IReadOnlyList<Track>>(
                uriBuilder.Build(),
                HttpMethod.Get,
                content: null,
                accessTokenProvider,
                cancellationToken);
        }

        public Task<IReadOnlyList<AudioFeatures>> GetAudioFeaturesForTracksAsync(
            IEnumerable<string> ids,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            var uriBuilder = new SpotifyUriBuilder($"{BaseUrl}/audio-features")
                .AppendJoinToQuery("ids", ',', ids);

            return SendAsync<IReadOnlyList<AudioFeatures>>(
                uriBuilder.Build(),
                HttpMethod.Get,
                content: null,
                accessTokenProvider,
                cancellationToken);
        }

        public Task<Track> GetTrackAsync(
            string id,
            CountryCode? market = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            var uriBuilder = new SpotifyUriBuilder($"{BaseUrl}/tracks/{id}")
                .AppendToQueryIfNotNull("market", market);

            return SendAsync<Track>(
                uriBuilder.Build(),
                HttpMethod.Get,
                content: null,
                accessTokenProvider,
                cancellationToken);
        }

        public Task<AudioAnalysis> GetAudioAnalysisForTrackAsync(
            string id,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            return SendAsync<AudioAnalysis>(
                new($"{BaseUrl}/audio-analysis/{id}"),
                HttpMethod.Get,
                content: null,
                accessTokenProvider,
                cancellationToken);
        }

        public Task<AudioFeatures> GetAudioFeaturesForTrackAsync(
            string id,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            return SendAsync<AudioFeatures>(
                new($"{BaseUrl}/audio-features/{id}"),
                HttpMethod.Get,
                content: null,
                accessTokenProvider,
                cancellationToken);
        }

        #region ISpotifyTracksApi Implementation
        Task<IReadOnlyList<Track>> ISpotifyTracksApi.GetTracksAsync(
            IReadOnlyList<string> ids,
            CountryCode? market,
            CancellationToken cancellationToken)
        {
            return GetTracksAsync(ids, market, null, cancellationToken);
        }

        Task<IReadOnlyList<AudioFeatures>> ISpotifyTracksApi.GetAudioFeaturesForTracksAsync(
            IEnumerable<string> ids,
            CancellationToken cancellationToken)
        {
            return GetAudioFeaturesForTracksAsync(ids, null, cancellationToken);
        }

        Task<Track> ISpotifyTracksApi.GetTrackAsync(string id, CountryCode? market, CancellationToken cancellationToken)
        {
            return GetTrackAsync(id, market, null, cancellationToken);
        }

        Task<AudioAnalysis> ISpotifyTracksApi.GetAudioAnalysisForTrackAsync(string id, CancellationToken cancellationToken)
        {
            return GetAudioAnalysisForTrackAsync(id, null, cancellationToken);
        }

        Task<AudioFeatures> ISpotifyTracksApi.GetAudioFeaturesForTrackAsync(string id, CancellationToken cancellationToken)
        {
            return GetAudioFeaturesForTrackAsync(id, null, cancellationToken);
        }
        #endregion
    }
}
