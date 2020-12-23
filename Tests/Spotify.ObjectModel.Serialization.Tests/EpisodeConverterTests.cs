using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spotify.ObjectModel.Serialization.Tests.Resources;
using System.Text.Json;

namespace Spotify.ObjectModel.Serialization.Tests
{
    [TestClass]
    public class EpisodeConverterTests : JsonConverterTests<Episode>
    {
        protected override string TestJson => TestData.EpisodeJson;
        protected override JsonSerializerOptions SerializerOptions => new()
        {
            Converters =
            {
                new EpisodeConverter(),
                new ImageConverter(),
                new ResumePointConverter(),
                new SimplifiedShowConverter(),
                new CopyrightConverter(),
                new CountryCodeConverter(),
            }
        };
    }
}
