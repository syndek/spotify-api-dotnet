using System;
using System.Text.Json;
using System.Text.Json.Serialization;

using Spotify.ObjectModel.EnumConverters;
using Spotify.Web.Authorization;

namespace Spotify.ObjectModel.JsonConverters
{
    internal sealed class AccessTokenConverter : JsonConverter<AccessToken>
    {
        internal static readonly AccessTokenConverter Instance = new();

        private AccessTokenConverter() : base() { }

        public override AccessToken Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            reader.AssertTokenType(JsonTokenType.StartObject);

            String accessToken = String.Empty;
            Int32 expiresIn = default;
            AuthorizationScopes scope = default;

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    break;
                }

                if (reader.TokenType != JsonTokenType.PropertyName)
                {
                    throw new JsonException();
                }

                switch (reader.GetString())
                {
                    case "access_token":
                        accessToken = reader.ReadString()!;
                        break;
                    case "scope":
                        scope = AuthorizationScopeConverter.FromSpotifyStrings(reader.ReadString()!.Split(' ', StringSplitOptions.RemoveEmptyEntries));
                        break;
                    case "expires_in":
                        expiresIn = reader.ReadInt32();
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            return new(accessToken, scope, expiresIn);
        }

        public override void Write(Utf8JsonWriter writer, AccessToken value, JsonSerializerOptions options) => throw new NotSupportedException();
    }
}