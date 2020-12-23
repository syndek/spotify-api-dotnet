using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spotify.ObjectModel.Serialization.Tests.Resources;
using System.Text.Json;

namespace Spotify.ObjectModel.Serialization.Tests
{
    [TestClass]
    public class SimplifiedPlaylistConverterTests : JsonConverterTests<SimplifiedPlaylist>
    {
        // Currently, it looks like SimplifiedPlaylist is unused in the Spotify Web API.
        // As a result, we just use a regular Playlist JSON object to test this converter.
        // It should be identical to a SimplifiedPlaylist JSON object, with an extra 'followers' field that gets ignored.
        protected override string TestJson => TestData.PlaylistJson;
        protected override JsonSerializerOptions SerializerOptions => new()
        {
            Converters =
            {
                new SimplifiedPlaylistConverter(),
                new ImageConverter(),
                new PagingConverterFactory(),
                new PlaylistTrackConverter(),
                new PlayableConverter(),
                new TrackConverter(),
                new SimplifiedAlbumConverter(),
                new CountryCodeConverter(),
                new SimplifiedArtistConverter(),
                new PublicUserConverter()
            }
        };
    }
}
