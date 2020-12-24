using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spotify.ObjectModel.Serialization.Tests.Resources;

namespace Spotify.ObjectModel.Serialization.Tests
{
    [TestClass]
    public class ShowConverterTests : JsonConverterTests<Show>
    {
        protected override string TestJson => TestData.ShowJson;
        protected override JsonSerializerOptions SerializerOptions => new()
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
