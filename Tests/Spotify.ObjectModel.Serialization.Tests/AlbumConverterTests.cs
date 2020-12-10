using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spotify.ObjectModel.Serialization.Tests.Resources;
using System.Text.Json;

namespace Spotify.ObjectModel.Serialization.Tests
{
    [TestClass]
    public class AlbumConverterTests : JsonConverterTests<Album>
    {
        public override string TestJson => TestData.AlbumJson;
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