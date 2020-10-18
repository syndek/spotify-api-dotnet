using System;
using System.Text.Json;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Spotify.ObjectModel.Serialization.Tests.Resources;

namespace Spotify.ObjectModel.Serialization.Tests
{
    [TestClass]
    public class TrackConverterTests : JsonConverterTests<Track>
    {
        public override String TestJson => TestData.TrackJson;
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