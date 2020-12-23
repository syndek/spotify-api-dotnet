using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spotify.Web.Authorization.Serialization
{
    public class AccessTokenConverter : JsonConverter<AccessToken>
    {
        public override AccessToken Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType is not JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            string accessToken = string.Empty;
            string scope = string.Empty;
            int expiresIn = default;

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
                    default:
                        reader.Skip();
                        break;
                }
            }

            return new(
                accessToken,
                scope == string.Empty ? 0 : AuthorizationScopesConverter.FromSpotifyStrings(scope.Split(' ')),
                expiresIn);
        }

        public override void Write(Utf8JsonWriter writer, AccessToken value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteString("access_token", value.Value);
            writer.WriteString("token_type", "Bearer");
            writer.WriteString("scope", string.Join(' ', value.Scope.ToSpotifyStrings()));
            writer.WriteNumber("expires_in", value.ExpiresAt.Second - DateTime.Now.Second);

            writer.WriteEndObject();
        }
    }
}
