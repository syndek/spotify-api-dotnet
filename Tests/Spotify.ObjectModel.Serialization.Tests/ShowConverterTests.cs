using System;
using System.Text.Json;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Spotify.ObjectModel.Serialization.Tests.Resources;

namespace Spotify.ObjectModel.Serialization.Tests
{
    [TestClass]
    public class ShowConverterTests : JsonConverterTests<Show>
    {
        public override String TestJson => TestData.ShowJson;
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