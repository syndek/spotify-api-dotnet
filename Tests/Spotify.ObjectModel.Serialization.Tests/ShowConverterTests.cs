using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spotify.ObjectModel.Serialization.Tests.Resources;
using System.Text.Json;

namespace Spotify.ObjectModel.Serialization.Tests
{
    [TestClass]
    public class ShowConverterTests : JsonConverterTests<Show>
    {
        public override string TestJson => TestData.ShowJson;
        public override JsonSerializerOptions SerializerOptions => new()
        {
            Converters =
            {
                new ShowConverter(),
                new CopyrightConverter(),
                new CountryCodeConverter(),
                new ImageConverter(),
                new PagingConverterFactory(),
                new SimplifiedEpisodeConverter(),
                new ResumePointConverter()
            }
        };
    }
}