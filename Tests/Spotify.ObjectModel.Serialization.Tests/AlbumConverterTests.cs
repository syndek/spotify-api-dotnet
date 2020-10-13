using System;
using System.Text.Json;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Spotify.ObjectModel.Serialization.Tests.Resources;

namespace Spotify.ObjectModel.Serialization.Tests
{
    [TestClass]
    public class AlbumConverterTests : JsonConverterTests<Album>
    {
        public override String TestJson => TestData.AlbumJson;
        public override JsonSerializerOptions SerializerOptions => new()
        {
            Converters =
            {
                new AlbumConverter(),
                new ImageConverter(),
                new SimplifiedArtistConverter(),
                new PagingConverterFactory(),
                new SimplifiedTrackConverter(),
                new CountryCodeConverter(),
                new CopyrightConverter()
            }
        };
    }
}