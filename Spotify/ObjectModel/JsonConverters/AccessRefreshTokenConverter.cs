using System;
using System.Text.Json;
using System.Text.Json.Serialization;

using Spotify.ObjectModel.EnumConverters;
using Spotify.Web.Authorization;

namespace Spotify.ObjectModel.JsonConverters
{
    internal sealed class AccessRefreshTokenConverter : JsonConverter<AccessRefreshToken>
    {
        internal static readonly AccessRefreshTokenConverter Instance = new();

        private AccessRefreshTokenConverter() : base() { }

        public override AccessRefreshToken Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            reader.AssertTokenType(JsonTokenType.StartObject);

            String accessToken = String.Empty;
            String refreshToken = String.Empty;
            AuthorizationScopes scope = default;
            Int32 expiresIn = default;

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
                    case "refresh_token":
                        refreshToken = reader.ReadString()!;
                        break;
                    case "scope":
                        scope = AuthorizationScopeConverter.FromSpotifyStrings(
                            reader.ReadString()!.Split(' ', StringSplitOptions.RemoveEmptyEntries));
                        break;
                    case "expires_in":
                        expiresIn = reader.ReadInt32();
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            return new(accessToken, refreshToken, scope, expiresIn);
        }

        public override void Write(Utf8JsonWriter writer, AccessRefreshToken value, JsonSerializerOptions options) => throw new NotSupportedException();
    }
}