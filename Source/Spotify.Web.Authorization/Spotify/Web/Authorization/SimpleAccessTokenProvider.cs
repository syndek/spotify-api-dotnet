using System;
using System.Threading;
using System.Threading.Tasks;

namespace Spotify.Web.Authorization
{
    /// <summary>
    /// Represents a <see cref="IAccessTokenProvider"/> that simply returns the <see cref="string"/> it was constructed with.
    /// </summary>
    public class SimpleAccessTokenProvider : object, IAccessTokenProvider
    {
        private readonly string accessToken;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleAccessTokenProvider"/> class with the specified <paramref name="accessToken"/>.
        /// </summary>
        /// <param name="accessToken">A <see cref="string"/> representing the access token to use.</param>
        public SimpleAccessTokenProvider(string accessToken) : base()
        {
            this.accessToken = accessToken;
        }

        /// <inheritdoc/>
        public ValueTask<AccessToken> GetAccessTokenAsync(CancellationToken cancellationToken = default) =>
            ValueTask.FromResult(new AccessToken(this.accessToken, default, default));
    }
}