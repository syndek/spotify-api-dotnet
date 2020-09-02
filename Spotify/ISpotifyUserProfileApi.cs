using System;
using System.Threading;
using System.Threading.Tasks;

using Spotify.ObjectModel;
using Spotify.Web.Authorization;

namespace Spotify
{
    /// <summary>
    /// Defines methods for retrieving information about a user’s profile.
    /// </summary>
    public interface ISpotifyUserProfileApi
    {
        Task<PrivateUser> GetCurrentUserProfileAsync(
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default);

        Task<PublicUser> GetUserProfileAsync(
            String id,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default);
    }
}