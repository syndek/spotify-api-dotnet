using System;
using System.Threading;
using System.Threading.Tasks;

using Spotify.ObjectModel;

namespace Spotify
{
    /// <summary>
    /// Defines methods for retrieving Spotify catalog information about albums,
    /// artists, playlists, tracks, shows or episodes that match a keyword string.
    /// </summary>
    public interface ISpotifySearchApi
    {
        Task<SearchResult> SearchAsync(
            string query,
            SearchResultTypes types,
            CountryCode? market = null,
            int? limit = null,
            int? offset = null,
            bool? includeExternal = null,
            CancellationToken cancellationToken = default);
    }
}