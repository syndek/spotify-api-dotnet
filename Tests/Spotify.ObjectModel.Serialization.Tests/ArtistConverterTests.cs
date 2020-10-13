using System;
using System.Text.Json;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Spotify.ObjectModel.Serialization.Tests.Resources;

namespace Spotify.ObjectModel.Serialization.Tests
{
    [TestClass]
    public class ArtistConverterTests : JsonConverterTests<Artist>
    {
        public override String TestJson => TestData.ArtistJson;
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