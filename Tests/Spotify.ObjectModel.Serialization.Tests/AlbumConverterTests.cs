using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spotify.ObjectModel.Serialization.Tests.Resources;
using System.Text.Json;

namespace Spotify.ObjectModel.Serialization.Tests
{
    [TestClass]
    public class AlbumConverterTests : JsonConverterTests<Album>
    {
        protected override string TestJson => TestData.AlbumJson;
        protected override JsonSerializerOptions SerializerOptions => new()
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