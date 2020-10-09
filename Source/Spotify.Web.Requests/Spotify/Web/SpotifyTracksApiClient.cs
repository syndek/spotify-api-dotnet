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
            IReadOnlyList<String> ids,
            CountryCode? market = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            var uriBuilder = new SpotifyUriBuilder($"{SpotifyApiClient.BaseUrl}/tracks")
                .AppendJoinToQuery("ids", ',', ids)
                .AppendToQueryIfNotNull("market", market);

            return base.SendAsync<IReadOnlyList<Track>>(
                uriBuilder.Build(),
                HttpMethod.Get,
                content: null,
                accessTokenProvider,
                cancellationToken);
        }

        public Task<IReadOnlyList<AudioFeatures>> GetAudioFeaturesForTracksAsync(
            IEnumerable<String> ids,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            var uriBuilder = new SpotifyUriBuilder($"{SpotifyApiClient.BaseUrl}/audio-features")
                .AppendJoinToQuery("ids", ',', ids);

            return base.SendAsync<IReadOnlyList<AudioFeatures>>(
                uriBuilder.Build(),
                HttpMethod.Get,
                content: null,
                accessTokenProvider,
                cancellationToken);
        }

        public Task<Track> GetTrackAsync(
            String id,
            CountryCode? market = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            var uriBuilder = new SpotifyUriBuilder($"{SpotifyApiClient.BaseUrl}/tracks/{id}")
                .AppendToQueryIfNotNull("market", market);

            return base.SendAsync<Track>(
                uriBuilder.Build(),
                HttpMethod.Get,
                content: null,
                accessTokenProvider,
                cancellationToken);
        }

        public Task<AudioAnalysis> GetAudioAnalysisForTrackAsync(
            String id,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            return base.SendAsync<AudioAnalysis>(
                new($"{SpotifyApiClient.BaseUrl}/audio-analysis/{id}"),
                HttpMethod.Get,
                content: null,
                accessTokenProvider,
                cancellationToken);
        }

        public Task<AudioFeatures> GetAudioFeaturesForTrackAsync(
            String id,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            return base.SendAsync<AudioFeatures>(
                new($"{SpotifyApiClient.BaseUrl}/audio-features/{id}"),
                HttpMethod.Get,
                content: null,
                accessTokenProvider,
                cancellationToken);
        }

        #region ISpotifyTracksApi Implementation
        Task<IReadOnlyList<Track>> ISpotifyTracksApi.GetTracksAsync(
            IReadOnlyList<String> ids,
            CountryCode? market,
            CancellationToken cancellationToken)
        {
            return this.GetTracksAsync(ids, market, null, cancellationToken);
        }

        Task<IReadOnlyList<AudioFeatures>> ISpotifyTracksApi.GetAudioFeaturesForTracksAsync(
            IEnumerable<String> ids,
            CancellationToken cancellationToken)
        {
            return this.GetAudioFeaturesForTracksAsync(ids, null, cancellationToken);
        }

        Task<Track> ISpotifyTracksApi.GetTrackAsync(String id, CountryCode? market, CancellationToken cancellationToken)
        {
            return this.GetTrackAsync(id, market, null, cancellationToken);
        }

        Task<AudioAnalysis> ISpotifyTracksApi.GetAudioAnalysisForTrackAsync(String id, CancellationToken cancellationToken)
        {
            return this.GetAudioAnalysisForTrackAsync(id, null, cancellationToken);
        }

        Task<AudioFeatures> ISpotifyTracksApi.GetAudioFeaturesForTrackAsync(String id, CancellationToken cancellationToken)
        {
            return this.GetAudioFeaturesForTrackAsync(id, null, cancellationToken);
        }
        #endregion
    }
}