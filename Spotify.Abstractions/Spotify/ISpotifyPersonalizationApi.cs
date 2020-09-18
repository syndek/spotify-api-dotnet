using System;
using System.Threading;
using System.Threading.Tasks;

using Spotify.ObjectModel;

namespace Spotify
{
    /// <summary>
    /// Defines methods for accessing a Spotify user's personalization data.
    /// </summary>
    public interface ISpotifyPersonalizationApi
    {
        public Task<Paging<Artist>> GetTopArtistsForCurrentUserAsync(
            Int32? limit = null,
            Int32? offset = null,
            TimeRange? timeRange = null,
            CancellationToken cancellationToken = default);

        public Task<Paging<Track>> GetTopTracksForCurrentUserAsync(
            Int32? limit = null,
            Int32? offset = null,
            TimeRange? timeRange = null,
            CancellationToken cancellationToken = default);
    }
}