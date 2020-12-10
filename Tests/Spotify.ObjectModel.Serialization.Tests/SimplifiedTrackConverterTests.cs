using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spotify.ObjectModel.Serialization.Tests.Resources;
using System.Text.Json;

namespace Spotify.ObjectModel.Serialization.Tests
{
    [TestClass]
    public class SimplifiedTrackConverterTests : JsonConverterTests<SimplifiedTrack>
    {
        public override string TestJson => TestData.TrackJson;
        public override JsonSerializerOptions SerializerOptions => new()
        {
            Converters =
            {
                new SimplifiedTrackConverter(),
                new CountryCodeConverter(),
                new SimplifiedArtistConverter()
            }
        };
    }
}