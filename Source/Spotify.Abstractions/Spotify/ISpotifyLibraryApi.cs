using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Spotify.ObjectModel;

namespace Spotify
{
    /// <summary>
    /// Defines methods for managing the current user's Spotify library.
    /// </summary>
    public interface ISpotifyLibraryApi
    {
        Task SaveAlbumsAsync(
            IEnumerable<String> ids,
            CancellationToken cancellationToken = default);

        Task RemoveAlbumsAsync(
            IEnumerable<String> ids,
            CancellationToken cancellationToken = default);

        Task<Paging<Saved<Album>>> GetSavedAlbumsAsync(
            Int32? limit = null,
            Int32? offset = null,
            CountryCode? market = null,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Boolean>> CheckSavedAlbumsAsync(
            IEnumerable<String> ids,
            CancellationToken cancellationToken = default);

        Task SaveTracksAsync(
            IEnumerable<String> ids,
            CancellationToken cancellationToken = default);

        Task RemoveTracksAsync(
            IEnumerable<String> ids,
            CancellationToken cancellationToken = default);

        Task<Paging<Saved<Track>>> GetSavedTracksAsync(
            Int32? limit = null,
            Int32? offset = null,
            CountryCode? market = null,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Boolean>> CheckSavedTracksAsync(
            IEnumerable<String> ids,
            CancellationToken cancellationToken = default);

        Task SaveShowsAsync(
            IEnumerable<String> ids,
            CancellationToken cancellationToken = default);

        Task RemoveShowsAsync(
            IEnumerable<String> ids,
            CountryCode? market = null,
            CancellationToken cancellationToken = default);

        Task<Paging<Saved<Show>>> GetSavedShowsAsync(
            Int32? limit = null,
            Int32? offset = null,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Boolean>> CheckSavedShowsAsync(
            IEnumerable<String> ids,
            CancellationToken cancellationToken = default);
    }
}