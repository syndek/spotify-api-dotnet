using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spotify.ObjectModel.Serialization.Tests.Resources;

namespace Spotify.ObjectModel.Serialization.Tests
{
    [TestClass]
    public class PlayableConverterTests
    {
        [TestMethod]
        public void RoundTripSerialization_Episode()
        {
            var options = new JsonSerializerOptions
            {
                Converters =
                {
                    new PlayableConverter(),
                    new EpisodeConverter(),
                    new ImageConverter(),
                    new ResumePointConverter(),
                    new SimplifiedShowConverter(),
                    new CopyrightConverter(),
                    new CountryCodeConverter(),
                }
            };

            var deserialized = JsonSerializer.Deserialize<IPlayable>(TestData.EpisodeJson, options);

            Assert.IsInstanceOfType(deserialized, typeof(Episode));

            var reserialized = JsonSerializer.Serialize(deserialized, options);

            Assert.AreEqual(
                expected: deserialized,
                actual: JsonSerializer.Deserialize<IPlayable>(reserialized, options));
        }

        [TestMethod]
        public void RoundTripSerialization_Track()
        {
            var options = new JsonSerializerOptions
            {
                Converters =
                {
                    new PlayableConverter(),
                    new TrackConverter(),
                    new CountryCodeConverter(),
                    new SimplifiedAlbumConverter(),
                    new ImageConverter(),
                    new SimplifiedArtistConverter()
                }
            };

            var deserialized = JsonSerializer.Deserialize<IPlayable>(TestData.TrackJson, options);

            Assert.IsInstanceOfType(deserialized, typeof(Track));

            var reserialized = JsonSerializer.Serialize(deserialized, options);

            Assert.AreEqual(
                expected: deserialized,
                actual: JsonSerializer.Deserialize<IPlayable>(reserialized, options));
        }

        [TestMethod]
        public void NonIPlayableType()
        {
            var options = new JsonSerializerOptions() { Converters = { new PlayableConverter() } };

            Assert.ThrowsException<JsonException>(() => JsonSerializer.Deserialize<IPlayable>(TestData.AlbumJson, options));
        }
    }
}
