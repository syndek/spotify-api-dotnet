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

        /// <inheritdoc/>
        public Task<IReadOnlyList<Track>> GetTracksAsync(
            IReadOnlyList<String> ids,
            CountryCode? market = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<IReadOnlyList<AudioFeatures>> GetAudioFeaturesForTracksAsync(
            IEnumerable<String> ids,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<Track> GetTrackAsync(
            String id,
            CountryCode? market = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<AudioAnalysis> GetAudioAnalysisForTrackAsync(
            String id,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<AudioFeatures> GetAudioFeaturesForTrackAsync(
            String id,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}