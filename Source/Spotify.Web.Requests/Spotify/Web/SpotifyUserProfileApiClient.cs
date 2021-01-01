using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Spotify.ObjectModel;
using Spotify.Web.Authorization;

namespace Spotify.Web
{
    /// <summary>
    /// Represents a <see cref="SpotifyApiClient"/> for retrieving information about a user’s profile.
    /// </summary>
    public class SpotifyUserProfileApiClient : SpotifyApiClient, ISpotifyUserProfileApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpotifyUserProfileApiClient"/> class with the specified <paramref name="httpClient"/>.
        /// </summary>
        /// <param name="httpClient">An instance of <see cref="HttpClient"/> to use for requests to the Spotify Web API.</param>
        public SpotifyUserProfileApiClient(HttpClient httpClient) : base(httpClient) { }

        /// <summary>
        /// Asynchronously get the current <see cref="PrivateUser">User</see>'s profile.
        /// If there is no user associated with the request, this operation will fail.
        /// </summary>
        /// <param name="accessTokenProvider">An optional <see cref="IAccessTokenProvider"/> to use instead of the default.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.</returns>
        public Task<PrivateUser> GetCurrentUserProfileAsync(
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            return SendAsync<PrivateUser>(
                new($"{BaseUrl}/me"),
                HttpMethod.Get,
                content: null,
                accessTokenProvider,
                cancellationToken);
        }

        /// <summary>
        /// Asynchronously get a <see cref="PublicUser">User</see>'s profile.
        /// </summary>
        /// <param name="id">The Spotify ID of the <see cref="PublicUser">User</see> to get.</param>
        /// <param name="accessTokenProvider">An optional <see cref="IAccessTokenProvider"/> to use instead of the default.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.</returns>
        public Task<PublicUser> GetUserProfileAsync(
            string id,
            IAccessTokenProvider? accessTokenProvider = null,
            CancellationToken cancellationToken = default)
        {
            return SendAsync<PublicUser>(
                new($"{BaseUrl}/users/{id}"),
                HttpMethod.Get,
                content: null,
                accessTokenProvider,
                cancellationToken);
        }

        #region ISpotifyUserProfileApi Implementation
        Task<PrivateUser> ISpotifyUserProfileApi.GetCurrentUserProfileAsync(CancellationToken cancellationToken)
        {
            return GetCurrentUserProfileAsync(null, cancellationToken);
        }

        Task<PublicUser> ISpotifyUserProfileApi.GetUserProfileAsync(string id, CancellationToken cancellationToken)
        {
            return GetUserProfileAsync(id, null, cancellationToken);
        }
        #endregion
    }
}
