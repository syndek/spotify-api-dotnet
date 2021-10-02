using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spotify.Web.Authorization.Serialization
{
    public class AccessRefreshTokenConverter : JsonConverter<AccessRefreshToken>
    {
        public override AccessRefreshToken Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType is not JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            string accessToken = string.Empty;
            string scope = string.Empty;
            int expiresIn = default;
            string? refreshToken = null;

            while (reader.Read())
            {
                if (reader.TokenType is JsonTokenType.EndObject)
                {
                    break;
                }

                if (reader.TokenType is not JsonTokenType.PropertyName)
                {
                    throw new JsonException();
                }

                var propertyName = reader.GetString();

                reader.Read(); // Read to next token.

                switch (propertyName)
                {
                    case "access_token":
                        accessToken = reader.GetString()!;
                        break;
                    case "scope":
                        scope = reader.GetString()!;
                        break;
                    case "expires_in":
                        expiresIn = reader.GetInt32();
                        break;
                    case "refresh_token":
                        refreshToken = reader.GetString();
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            return new(
                accessToken,
                scope == string.Empty ? 0 : AuthorizationScopesConverter.FromSpotifyStrings(scope.Split(' ')),
                expiresIn,
                refreshToken);
        }

        public override void Write(Utf8JsonWriter writer, AccessRefreshToken value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteString("access_token", value.AccessToken.Value);
            writer.WriteString("token_type", "Bearer");
            writer.WriteString("scope", string.Join(' ', value.AccessToken.Scope.ToSpotifyStrings()));
            writer.WriteNumber("expires_in", value.AccessToken.ExpiresAt.Second - DateTime.Now.Second);
            writer.WriteString("refresh_token", value.RefreshToken);

            writer.WriteEndObject();
        }
    }
}
