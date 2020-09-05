using System;
using System.Text.Json;

namespace Spotify.ObjectModel.JsonConverters
{
    internal static class Utf8JsonReaderExtensions : Object
    {
        internal static Boolean ReadBoolean(ref this Utf8JsonReader reader)
        {
            reader.Read();
            return reader.GetBoolean();
        }

        internal static Byte ReadByte(ref this Utf8JsonReader reader)
        {
            reader.Read();
            return reader.GetByte();
        }

        internal static SByte ReadSByte(ref this Utf8JsonReader reader)
        {
            reader.Read();
            return reader.GetSByte();
        }

        internal static Int16 ReadInt16(ref this Utf8JsonReader reader)
        {
            reader.Read();
            return reader.GetInt16();
        }

        internal static UInt16 ReadUInt16(ref this Utf8JsonReader reader)
        {
            reader.Read();
            return reader.GetUInt16();
        }

        internal static Int32 ReadInt32(ref this Utf8JsonReader reader)
        {
            reader.Read();
            return reader.GetInt32();
        }

        internal static UInt32 ReadUInt32(ref this Utf8JsonReader reader)
        {
            reader.Read();
            return reader.GetUInt32();
        }

        internal static Int64 ReadInt64(ref this Utf8JsonReader reader)
        {
            reader.Read();
            return reader.GetInt64();
        }

        internal static UInt64 ReadUInt64(ref this Utf8JsonReader reader)
        {
            reader.Read();
            return reader.GetUInt64();
        }

        internal static Single ReadSingle(ref this Utf8JsonReader reader)
        {
            reader.Read();
            return reader.GetSingle();
        }

        internal static Double ReadDouble(ref this Utf8JsonReader reader)
        {
            reader.Read();
            return reader.GetDouble();
        }

        internal static Decimal ReadDecimal(ref this Utf8JsonReader reader)
        {
            reader.Read();
            return reader.GetDecimal();
        }

        internal static Guid ReadGuid(ref this Utf8JsonReader reader)
        {
            reader.Read();
            return reader.GetGuid();
        }

        internal static DateTime ReadDateTime(ref this Utf8JsonReader reader)
        {
            reader.Read();
            return reader.GetDateTime();
        }

        internal static DateTimeOffset ReadDateTimeOffset(ref this Utf8JsonReader reader)
        {
            reader.Read();
            return reader.GetDateTimeOffset();
        }

        internal static Byte[] ReadBytesFromBase64(ref this Utf8JsonReader reader)
        {
            reader.Read();
            return reader.GetBytesFromBase64();
        }

        internal static String? ReadString(ref this Utf8JsonReader reader)
        {
            reader.Read();
            return reader.GetString();
        }

        internal static String ReadComment(ref this Utf8JsonReader reader)
        {
            reader.Read();
            return reader.GetComment();
        }

        internal static String ReadPropertyName(ref this Utf8JsonReader reader)
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
        internal static Boolean Read(ref this Utf8JsonReader reader, JsonTokenType expectedTokenType)
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
        internal static void AssertTokenType(ref this Utf8JsonReader reader, JsonTokenType expectedTokenType)
        {
            if (reader.TokenType != expectedTokenType)
            {
                throw new JsonException();
            }
        }
    }
}