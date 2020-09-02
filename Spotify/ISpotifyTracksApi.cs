using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Spotify.ObjectModel;
using Spotify.Web.Authorization;

namespace Spotify
{
    /// <summary>
    /// Defines methods for retrieving information about one or more tracks from the Spotify catalog.
    /// </summary>
    public interface ISpotifyTracksApi
    {
        Task<IReadOnlyList<Track>> GetTracksAsync(
            IReadOnlyList<String> ids,
            CountryCode? market = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<AudioFeatures>> GetAudioFeaturesForTracksAsync(
            IEnumerable<String> ids,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default);

        Task<Track> GetTrackAsync(
            String id,
            CountryCode? market = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default);

        Task<AudioAnalysis> GetAudioAnalysisForTrackAsync(
            String id,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default);

        Task<AudioFeatures> GetAudioFeaturesForTrackAsync(
            String id,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default);
    }
}