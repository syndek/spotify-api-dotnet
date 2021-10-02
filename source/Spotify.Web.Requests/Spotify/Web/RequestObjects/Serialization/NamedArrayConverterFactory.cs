using System;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spotify.Web.RequestObjects.Serialization
{
    internal class NamedArrayConverterFactory : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert) =>
            typeToConvert.IsGenericType && typeToConvert.GetGenericTypeDefinition() == typeof(NamedArray<>);

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            if (!CanConvert(typeToConvert))
            {
                throw new ArgumentException($"{typeToConvert} must be a generic NamedArray type.", nameof(typeToConvert));
            }

            var elementType = typeToConvert.GetGenericArguments()[0];

            return (JsonConverter)Activator.CreateInstance(
                typeof(NamedArrayConverter<>).MakeGenericType(elementType),
                BindingFlags.Instance | BindingFlags.Public,
                null,
                null,
                null)!;
        }
    }
}
