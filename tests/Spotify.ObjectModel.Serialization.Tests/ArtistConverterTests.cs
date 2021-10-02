using System.Text.Json;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Spotify.ObjectModel.Serialization.Tests.Resources;

namespace Spotify.ObjectModel.Serialization.Tests
{
    [TestClass]
    public class ArtistConverterTests : JsonConverterTests<Artist>
    {
        protected override string TestJson => TestData.ArtistJson;
        protected override JsonSerializerOptions SerializerOptions => new JsonSerializerOptions
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
