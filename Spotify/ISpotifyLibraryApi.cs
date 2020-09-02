using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Spotify.ObjectModel;
using Spotify.Web.Authorization;

namespace Spotify
{
    /// <summary>
    /// Defines methods for managing the current user's Spotify library.
    /// </summary>
    public interface ISpotifyLibraryApi
    {
        Task SaveAlbumsAsync(
            IEnumerable<String> ids,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default);

        Task RemoveAlbumsAsync(
            IEnumerable<String> ids,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default);

        Task<Paging<Saved<Album>>> GetSavedAlbumsAsync(
            Int32? limit = null,
            Int32? offset = null,
            CountryCode? market = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Boolean>> CheckSavedAlbumsAsync(
            IEnumerable<String> ids,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default);

        Task SaveTracksAsync(
            IEnumerable<String> ids,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default);

        Task RemoveTracksAsync(
            IEnumerable<String> ids,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default);

        Task<Paging<Saved<Track>>> GetSavedTracksAsync(
            Int32? limit = null,
            Int32? offset = null,
            CountryCode? market = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Boolean>> CheckSavedTracksAsync(
            IEnumerable<String> ids,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default);

        Task SaveShowsAsync(
            IEnumerable<String> ids,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default);

        Task RemoveShowsAsync(
            IEnumerable<String> ids,
            CountryCode? market = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default);

        Task<Paging<Saved<Show>>> GetSavedShowsAsync(
            Int32? limit = null,
            Int32? offset = null,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Boolean>> CheckSavedShowsAsync(
            IEnumerable<String> ids,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default);
    }
}