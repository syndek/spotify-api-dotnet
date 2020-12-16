using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json;

namespace Spotify.ObjectModel.Serialization.Tests
{
    public abstract class JsonConverterTests<TObject>
    {
        protected abstract string TestJson { get; }
        protected abstract JsonSerializerOptions SerializerOptions { get; }

        [TestMethod]
        public void RoundTripSerialization()
        {
            var deserialized = JsonSerializer.Deserialize<TObject>(TestJson, SerializerOptions);
            var reserialized = JsonSerializer.Serialize(deserialized, SerializerOptions);

            Assert.AreEqual(
                expected: deserialized,
                actual: JsonSerializer.Deserialize<TObject>(reserialized, SerializerOptions));
        }
    }
}