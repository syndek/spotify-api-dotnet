using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spotify.ObjectModel.Serialization.Tests.Resources;

namespace Spotify.ObjectModel.Serialization.Tests
{
    [TestClass]
    public class SimplifiedEpisodeConverterTests : JsonConverterTests<SimplifiedEpisode>
    {
        protected override string TestJson => TestData.SimplifiedEpisodeJson;
        protected override JsonSerializerOptions SerializerOptions => new()
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
