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
