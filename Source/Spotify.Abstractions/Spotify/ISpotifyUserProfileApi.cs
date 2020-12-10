using Spotify.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace Spotify
{
    /// <summary>
    /// Defines methods for retrieving information about a user’s profile.
    /// </summary>
    public interface ISpotifyUserProfileApi
    {
        Task<PrivateUser> GetCurrentUserProfileAsync(
            CancellationToken cancellationToken = default);

        Task<PublicUser> GetUserProfileAsync(
            string id,
            CancellationToken cancellationToken = default);
    }
}