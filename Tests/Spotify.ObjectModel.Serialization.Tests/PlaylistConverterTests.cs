using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spotify.ObjectModel.Serialization.Tests.Resources;
using System.Text.Json;

namespace Spotify.ObjectModel.Serialization.Tests
{
    [TestClass]
    public class PlaylistConverterTests : JsonConverterTests<Playlist>
    {
        public override string TestJson => TestData.PlaylistJson;
        public override JsonSerializerOptions SerializerOptions => new()
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