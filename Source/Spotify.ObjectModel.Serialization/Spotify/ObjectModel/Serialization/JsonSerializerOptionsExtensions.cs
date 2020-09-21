using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spotify.ObjectModel.Serialization
{
    internal static class JsonSerializerOptionsExtensions : Object
    {
        public static JsonConverter<TValue> GetConverter<TValue>(this JsonSerializerOptions options) =>
            (JsonConverter<TValue>) options.GetConverter(typeof(TValue));
    }
}