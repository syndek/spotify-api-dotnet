using System.Text.Json;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Spotify.ObjectModel.Serialization.Tests.Resources;

namespace Spotify.ObjectModel.Serialization.Tests
{
    [TestClass]
    public class PlaylistConverterTests : JsonConverterTests<Playlist>
    {
        protected override string TestJson => TestData.PlaylistJson;
        protected override JsonSerializerOptions SerializerOptions => new()
        {
            Converters =
            {
                new PlaylistConverter(),
                new FollowersConverter(),
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
