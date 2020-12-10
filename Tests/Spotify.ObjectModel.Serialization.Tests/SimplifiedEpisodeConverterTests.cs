using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spotify.ObjectModel.Serialization.Tests.Resources;
using System.Text.Json;

namespace Spotify.ObjectModel.Serialization.Tests
{
    [TestClass]
    public class SimplifiedEpisodeConverterTests : JsonConverterTests<SimplifiedEpisode>
    {
        public override string TestJson => TestData.SimplifiedEpisodeJson;
        public override JsonSerializerOptions SerializerOptions => new()
        {
            Converters =
            {
                new SimplifiedEpisodeConverter(),
                new ImageConverter(),
                new ResumePointConverter()
            }
        };
    }
}