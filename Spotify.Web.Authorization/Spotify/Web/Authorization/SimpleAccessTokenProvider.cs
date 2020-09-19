using System;
using System.Threading;
using System.Threading.Tasks;

namespace Spotify.Web.Authorization
{
    /// <summary>
    /// Represents a <see cref="IAccessTokenProvider"/> that simply returns the <see cref="String"/> it was constructed with.
    /// </summary>
    public class SimpleAccessTokenProvider : Object, IAccessTokenProvider
    {
        private readonly String accessToken;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleAccessTokenProvider"/> class with the specified <paramref name="accessToken"/>.
        /// </summary>
        /// <param name="accessToken">A <see cref="String"/> representing the access token to use.</param>
        public SimpleAccessTokenProvider(String accessToken) : base()
        {
            this.accessToken = accessToken;
        }

        /// <inheritdoc/>
        public ValueTask<AccessToken> GetAccessTokenAsync(CancellationToken cancellationToken = default) =>
            ValueTask.FromResult(new AccessToken(accessToken, default, default));
    }
}