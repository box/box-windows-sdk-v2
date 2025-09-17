using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Box.Sdk.Gen.Internal
{
    static class SimpleJsonSerializer
    {
        static JsonSerializerOptions _options;

        static SimpleJsonSerializer()
        {
            _options = SerializerOptions.getOptions();
            _options.Converters.Add(new DateOnlyJsonConverter());
        }

        public static SerializedData Serialize(object obj) => new SerializedData(obj);

        static (T, string) BaseDeserialize<T>(SerializedData? obj)
        {
            if (obj == null)
            {
                throw new BoxSdkException("Object to be deserialized cannot be null");
            }

            var objAsJson = obj.AsJson();
            var deserializedObject = JsonSerializer.Deserialize<T>(obj.AsJson(), _options);

            if (deserializedObject == null)
            {
                throw new BoxSdkException("Deserialized object cannot be null");
            }

            return (deserializedObject, objAsJson);
        }

        public static T Deserialize<T>(SerializedData? obj) where T : ISerializable
        {
            var (deserialized, objAsJson) = BaseDeserialize<T>(obj);
            deserialized.SetJson(objAsJson);
            return deserialized;
        }

        internal static T DeserializeWithoutRawJson<T>(SerializedData? obj)
        {
            return BaseDeserialize<T>(obj).Item1;
        }

        public static string SdToJson(SerializedData obj) => JsonSerializer.Serialize(obj.Data, _options);

        internal static object? ConvertJsonElement(JsonElement element)
        {
            return element.ValueKind switch
            {
                JsonValueKind.Object => ConvertJsonObject(element),
                JsonValueKind.Array => ConvertJsonArray(element),
                JsonValueKind.String => element.GetString(),
                JsonValueKind.Number => element.TryGetInt32(out int intValue) ? (object)intValue : element.GetDouble(),
                JsonValueKind.True => true,
                JsonValueKind.False => false,
                JsonValueKind.Null => null,
                _ => throw new JsonException($"Unexpected JsonValueKind: {element.ValueKind}")
            };
        }

        private static Dictionary<string, object?> ConvertJsonObject(JsonElement element)
        {
            var dict = new Dictionary<string, object?>();

            foreach (var property in element.EnumerateObject())
            {
                dict[property.Name] = ConvertJsonElement(property.Value);
            }

            return dict;
        }

        private static List<object?> ConvertJsonArray(JsonElement element)
        {
            var list = new List<object?>();

            foreach (var item in element.EnumerateArray())
            {
                list.Add(ConvertJsonElement(item));
            }

            return list;
        }

        internal static Dictionary<string, object?>? GetAllFields(ISerializable dto)
        {
            string? json = dto.GetJson();

            if (json == null)
            {
                return null;
            }

            var asRawDict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json, _options);

            if (asRawDict == null)
            {
                return null;
            }

            var parsedJson = new Dictionary<string, object?>();

            foreach (var kvp in asRawDict)
            {
                parsedJson[kvp.Key] = ConvertJsonElement(kvp.Value);
            }

            return parsedJson;
        }
    }

    //Remove when migrated to .NET 7
    internal sealed class DateOnlyJsonConverter : JsonConverter<DateOnly>
    {
        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateOnly.FromDateTime(reader.GetDateTime());
        }

        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        {
            var isoDate = value.ToString("O");
            writer.WriteStringValue(isoDate);
        }
    }

    class StringEnumConverter<T> : JsonConverter<StringEnum<T>> where T : struct, Enum
    {
        public override StringEnum<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using var document = JsonDocument.ParseValue(ref reader);
            var stringValue = document.RootElement.ToString();
            var enumType = typeof(T);
            var enumFields = enumType.GetFields();

            foreach (var field in enumType.GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

                if (attribute?.Description.Equals(stringValue, StringComparison.OrdinalIgnoreCase) == true)
                {
                    return new StringEnum<T>((T?)field.GetValue(null));
                }
            }

            return new StringEnum<T>(stringValue);
        }

        public override void Write(Utf8JsonWriter writer, StringEnum<T> value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value.StringValue, options);
            return;
        }
    }

    class StringEnumListConverter<T> : JsonConverter<IReadOnlyList<StringEnum<T>>> where T : struct, Enum
    {
        private readonly JsonConverter<StringEnum<T>> _singleConverter;

        public StringEnumListConverter()
        {
            _singleConverter = new StringEnumConverter<T>();
        }

        public override IReadOnlyList<StringEnum<T>> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var list = new List<StringEnum<T>>();

            if (reader.TokenType != JsonTokenType.StartArray)
                throw new JsonException();

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndArray)
                    return list;

                if (reader.TokenType == JsonTokenType.String)
                {
                    var element = _singleConverter.Read(ref reader, typeof(StringEnum<T>), options);
                    if (element != null)
                    {
                        list.Add(element);
                    }
                }
            }

            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, IReadOnlyList<StringEnum<T>> value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();

            foreach (var item in value)
            {
                _singleConverter.Write(writer, item, options);
            }

            writer.WriteEndArray();
        }
    }

    class StringEnumNestedListConverter<T> : JsonConverter<IReadOnlyList<IReadOnlyList<StringEnum<T>>>> where T : struct, Enum
    {
        private readonly JsonConverter<IReadOnlyList<StringEnum<T>>> _innerListConverter;

        public StringEnumNestedListConverter()
        {
            _innerListConverter = new StringEnumListConverter<T>();
        }

        public override IReadOnlyList<IReadOnlyList<StringEnum<T>>> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var nestedList = new List<IReadOnlyList<StringEnum<T>>>();

            if (reader.TokenType != JsonTokenType.StartArray)
                throw new JsonException();

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndArray)
                    return nestedList;

                if (reader.TokenType == JsonTokenType.StartArray)
                {
                    var innerList = _innerListConverter.Read(ref reader, typeof(IReadOnlyList<StringEnum<T>>), options);
                    if (innerList != null)
                    {
                        nestedList.Add(innerList);
                    }
                }
            }

            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, IReadOnlyList<IReadOnlyList<StringEnum<T>>> value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();

            foreach (var innerList in value)
            {
                _innerListConverter.Write(writer, innerList, options);
            }

            writer.WriteEndArray();
        }
    }

    internal interface ISerializable
    {
        internal void SetJson(string json);
        internal string? GetJson();
    }
}
