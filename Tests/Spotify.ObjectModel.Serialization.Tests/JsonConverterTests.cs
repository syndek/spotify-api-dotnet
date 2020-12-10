using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json;

namespace Spotify.ObjectModel.Serialization.Tests
{
    public abstract class JsonConverterTests<TObject>
    {
        public abstract string TestJson { get; }
        public abstract JsonSerializerOptions SerializerOptions { get; }

        [TestMethod]
        public void RoundTripSerialization()
        {
            var deserialized = JsonSerializer.Deserialize<TObject>(this.TestJson, this.SerializerOptions);
            var reserialized = JsonSerializer.Serialize(deserialized, this.SerializerOptions);

            Assert.AreEqual(
                expected: deserialized,
                actual: JsonSerializer.Deserialize<TObject>(reserialized, this.SerializerOptions));
        }
    }
}