using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Spotify.ObjectModel;
using Spotify.ObjectModel.Collections;

namespace Spotify
{
    /// <summary>
    /// Defines methods for retrieving information about one or more shows from the Spotify catalog.
    /// </summary>
    public interface ISpotifyShowsApi
    {
        Task<IReadOnlyList<SimplifiedShow>> GetShowsAsync(
            IEnumerable<string> ids,
            CountryCode? market = null,
            CancellationToken cancellationToken = default);

        Task<Show> GetShowAsync(
            string id,
            CountryCode? market = null,
            CancellationToken cancellationToken = default);

        Task<Paging<SimplifiedEpisode>> GetShowEpisodesAsync(
            string id,
            int? limit = null,
            int? offset = null,
            CountryCode? market = null,
            CancellationToken cancellationToken = default);
    }
}
