using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spotify.ObjectModel.Serialization.Tests.Resources;
using System.Text.Json;

namespace Spotify.ObjectModel.Serialization.Tests
{
    [TestClass]
    public class SimplifiedAlbumConverterTests : JsonConverterTests<SimplifiedAlbum>
    {
        protected override string TestJson => TestData.SimplifiedAlbumJson;
        protected override JsonSerializerOptions SerializerOptions => new()
        {
            Converters =
            {
                new SimplifiedAlbumConverter(),
                new AlbumConverter(),
                new ImageConverter(),
                new SimplifiedArtistConverter(),
                new PagingConverterFactory(),
                new CountryCodeConverter(),
            }
        };
    }
}