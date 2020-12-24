using System.Threading;
using System.Threading.Tasks;
using Spotify.ObjectModel;

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
