using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spotify.ObjectModel.Serialization.Tests.Resources;
using System.Text.Json;

namespace Spotify.ObjectModel.Serialization.Tests
{
    [TestClass]
    public class TrackConverterTests : JsonConverterTests<Track>
    {
        public override string TestJson => TestData.TrackJson;
        public override JsonSerializerOptions SerializerOptions => new()
        {
            Converters =
            {
                new TrackConverter(),
                new CountryCodeConverter(),
                new SimplifiedAlbumConverter(),
                new ImageConverter(),
                new SimplifiedArtistConverter()
            }
        };
    }
}