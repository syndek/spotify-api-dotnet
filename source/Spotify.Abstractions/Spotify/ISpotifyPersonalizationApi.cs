using System.Threading;
using System.Threading.Tasks;

using Spotify.ObjectModel;
using Spotify.ObjectModel.Collections;

namespace Spotify
{
    /// <summary>
    /// Defines methods for accessing a Spotify user's personalization data.
    /// </summary>
    public interface ISpotifyPersonalizationApi
    {
        Task<Paging<Artist>> GetTopArtistsForCurrentUserAsync(
            int? limit = null,
            int? offset = null,
            TimeRange? timeRange = null,
            CancellationToken cancellationToken = default);

        Task<Paging<Track>> GetTopTracksForCurrentUserAsync(
            int? limit = null,
            int? offset = null,
            TimeRange? timeRange = null,
            CancellationToken cancellationToken = default);
    }
}
