using System.Threading;
using System.Threading.Tasks;

namespace Spotify.Web.Authorization
{
    /// <summary>
    /// Defines a method that can provide an <see cref="AccessToken"/>.
    /// </summary>
    public interface IAccessTokenProvider
    {
        /// <summary>
        /// Asynchronously retrieves an <see cref="AccessToken"/>.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests.</param>
        /// <returns>A <see cref="ValueTask{TResult}"/> representing the asynchronous operation.</returns>
        ValueTask<AccessToken> GetAccessTokenAsync(CancellationToken cancellationToken = default);
    }
}
