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
            String query,
            SearchResultTypes types,
            CountryCode? market = null,
            Int32? limit = null,
            Int32? offset = null,
            Boolean? includeExternal = null,
            CancellationToken cancellationToken = default);
    }
}