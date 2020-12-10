using Spotify.ObjectModel;
using Spotify.ObjectModel.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Spotify
{
    /// <summary>
    /// Defines methods for managing the current user's Spotify library.
    /// </summary>
    public interface ISpotifyLibraryApi
    {
        Task SaveAlbumsAsync(
            IEnumerable<string> ids,
            CancellationToken cancellationToken = default);

        Task RemoveAlbumsAsync(
            IEnumerable<string> ids,
            CancellationToken cancellationToken = default);

        Task<Paging<Saved<Album>>> GetSavedAlbumsAsync(
            int? limit = null,
            int? offset = null,
            CountryCode? market = null,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<bool>> CheckSavedAlbumsAsync(
            IEnumerable<string> ids,
            CancellationToken cancellationToken = default);

        Task SaveTracksAsync(
            IEnumerable<string> ids,
            CancellationToken cancellationToken = default);

        Task RemoveTracksAsync(
            IEnumerable<string> ids,
            CancellationToken cancellationToken = default);

        Task<Paging<Saved<Track>>> GetSavedTracksAsync(
            int? limit = null,
            int? offset = null,
            CountryCode? market = null,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<bool>> CheckSavedTracksAsync(
            IEnumerable<string> ids,
            CancellationToken cancellationToken = default);

        Task SaveShowsAsync(
            IEnumerable<string> ids,
            CancellationToken cancellationToken = default);

        Task RemoveShowsAsync(
            IEnumerable<string> ids,
            CountryCode? market = null,
            CancellationToken cancellationToken = default);

        Task<Paging<Saved<Show>>> GetSavedShowsAsync(
            int? limit = null,
            int? offset = null,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<bool>> CheckSavedShowsAsync(
            IEnumerable<string> ids,
            CancellationToken cancellationToken = default);
    }
}