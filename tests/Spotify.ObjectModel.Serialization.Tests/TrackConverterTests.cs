using System.Text.Json;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Spotify.ObjectModel.Serialization.Tests.Resources;

namespace Spotify.ObjectModel.Serialization.Tests
{
    [TestClass]
    public class TrackConverterTests : JsonConverterTests<Track>
    {
        protected override string TestJson => TestData.TrackJson;
        protected override JsonSerializerOptions SerializerOptions => new()
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
