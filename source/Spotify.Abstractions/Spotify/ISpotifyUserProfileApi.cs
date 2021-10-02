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
        /// <summary>
        /// Asynchronously get the current <see cref="PrivateUser">User</see>'s profile.
        /// If there is no user associated with the request, this operation will fail.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.</returns>
        Task<PrivateUser> GetCurrentUserProfileAsync(
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously get a <see cref="PublicUser">User</see>'s profile.
        /// </summary>
        /// <param name="id">The Spotify ID of the <see cref="PublicUser">User</see> to get.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.</returns>
        Task<PublicUser> GetUserProfileAsync(
            string id,
            CancellationToken cancellationToken = default);
    }
}
