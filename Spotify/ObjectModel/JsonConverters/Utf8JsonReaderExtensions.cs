using System;
using System.Text.Json;

namespace Spotify.ObjectModel.JsonConverters
{
    public static class Utf8JsonReaderExtensions : Object
    {
        public static Boolean ReadBoolean(ref this Utf8JsonReader reader)
        {
            reader.Read();
            return reader.GetBoolean();
        }

        public static Byte ReadByte(ref this Utf8JsonReader reader)
        {
            reader.Read();
            return reader.GetByte();
        }

        public static SByte ReadSByte(ref this Utf8JsonReader reader)
        {
            reader.Read();
            return reader.GetSByte();
        }

        public static Int16 ReadInt16(ref this Utf8JsonReader reader)
        {
            reader.Read();
            return reader.GetInt16();
        }

        public static UInt16 ReadUInt16(ref this Utf8JsonReader reader)
        {
            reader.Read();
            return reader.GetUInt16();
        }

        public static Int32 ReadInt32(ref this Utf8JsonReader reader)
        {
            reader.Read();
            return reader.GetInt32();
        }

        public static UInt32 ReadUInt32(ref this Utf8JsonReader reader)
        {
            reader.Read();
            return reader.GetUInt32();
        }

        public static Int64 ReadInt64(ref this Utf8JsonReader reader)
        {
            reader.Read();
            return reader.GetInt64();
        }

        public static UInt64 ReadUInt64(ref this Utf8JsonReader reader)
        {
            reader.Read();
            return reader.GetUInt64();
        }

        public static Single ReadSingle(ref this Utf8JsonReader reader)
        {
            reader.Read();
            return reader.GetSingle();
        }

        public static Double ReadDouble(ref this Utf8JsonReader reader)
        {
            reader.Read();
            return reader.GetDouble();
        }

        public static Decimal ReadDecimal(ref this Utf8JsonReader reader)
        {
            reader.Read();
            return reader.GetDecimal();
        }

        public static Guid ReadGuid(ref this Utf8JsonReader reader)
        {
            reader.Read();
            return reader.GetGuid();
        }

        public static DateTime ReadDateTime(ref this Utf8JsonReader reader)
        {
            reader.Read();
            return reader.GetDateTime();
        }

        public static DateTimeOffset ReadDateTimeOffset(ref this Utf8JsonReader reader)
        {
            reader.Read();
            return reader.GetDateTimeOffset();
        }

        public static Byte[] ReadBytesFromBase64(ref this Utf8JsonReader reader)
        {
            reader.Read();
            return reader.GetBytesFromBase64();
        }

        public static String? ReadString(ref this Utf8JsonReader reader)
        {
            reader.Read();
            return reader.GetString();
        }

        public static String ReadComment(ref this Utf8JsonReader reader)
        {
            reader.Read();
            return reader.GetComment();
        }

        public static String ReadPropertyName(ref this Utf8JsonReader reader)
        {
            reader.Read(JsonTokenType.PropertyName);
            return reader.GetString()!;
        }

        /// <summary>
        /// Reads the next JSON token from the specified <paramref name="reader"/> and throws a <see cref="JsonException"/>
        /// if the type of the token is not the type specified by <paramref name="expectedTokenType"/>.
        /// </summary>
        /// <param name="reader">The <see cref="Utf8JsonReader"/> to read from.</param>
        /// <param name="expectedTokenType">The expected <see cref="JsonTokenType"/> of the next token.</param>
        /// <returns><see langword="true"/> if a token was read successfully; otherwise, <see langword="false"/>.</returns>
        public static Boolean Read(ref this Utf8JsonReader reader, JsonTokenType expectedTokenType)
        {
            if (reader.Read())
            {
                reader.AssertTokenType(expectedTokenType);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Tests whether or not the current <see cref="Utf8JsonReader.TokenType"/> of the specified <paramref name="reader"/>
        /// is the specified <paramref name="expectedTokenType"/>, and throws a <see cref="JsonException"/> if not.
        /// </summary>
        /// <param name="reader">The</param>
        /// <param name="expectedTokenType"></param>
        /// <exception cref="JsonException">
        /// The current <see cref="Utf8JsonReader.TokenType"/> of <paramref name="reader"/> is not <paramref name="expectedTokenType"/>.
        /// </exception>
        public static void AssertTokenType(ref this Utf8JsonReader reader, JsonTokenType expectedTokenType)
        {
            if (reader.TokenType != expectedTokenType)
            {
                throw new JsonException();
            }
        }
    }
}