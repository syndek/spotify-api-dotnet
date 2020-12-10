using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spotify.ObjectModel.Serialization.Tests.Resources;
using System.Text.Json;

namespace Spotify.ObjectModel.Serialization.Tests
{
    [TestClass]
    public class ArtistConverterTests : JsonConverterTests<Artist>
    {
        public override string TestJson => TestData.ArtistJson;
        public override JsonSerializerOptions SerializerOptions => new JsonSerializerOptions
        {
            Converters =
            {
                new ArtistConverter(),
                new FollowersConverter(),
                new ImageConverter()
            }
        };
    }
}