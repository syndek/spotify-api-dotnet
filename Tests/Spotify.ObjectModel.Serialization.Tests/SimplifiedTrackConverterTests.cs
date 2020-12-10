using System;
using System.Text.Json;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Spotify.ObjectModel.Serialization.Tests.Resources;

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