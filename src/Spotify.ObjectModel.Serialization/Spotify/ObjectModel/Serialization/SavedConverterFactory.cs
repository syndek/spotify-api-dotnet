using System;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spotify.ObjectModel.Serialization
{
    public class SavedConverterFactory : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert) =>
            typeToConvert.IsGenericType && typeToConvert.GetGenericTypeDefinition() == typeof(Saved<>);

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            if (!CanConvert(typeToConvert))
            {
                throw new ArgumentException($"{nameof(typeToConvert)} must be a generic Saved type.", nameof(typeToConvert));
            }

            var elementType = typeToConvert.GetGenericArguments()[0];

            return (JsonConverter)Activator.CreateInstance(
                typeof(SavedConverter<>).MakeGenericType(elementType),
                BindingFlags.Instance | BindingFlags.Public,
                null,
                null,
                null)!;
        }
    }
}
