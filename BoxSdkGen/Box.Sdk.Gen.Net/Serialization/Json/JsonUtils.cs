using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;
using System.Text.Json;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen
{
    /// <summary>
    /// Class for various json utilities functions used in SDK.
    /// </summary>
    public static class JsonUtils
    {
        /// <summary>
        /// Converts Json string to SerializedData.
        /// </summary>
        /// <param name="text">json string</param>
        /// <returns>Serialized Data</returns>
        public static SerializedData JsonToSerializedData(string text)
        {
            return new SerializedData(text, true);
        }

        /// <summary>
        /// Converts SerializedData to Json string representation.
        /// </summary>
        /// <param name="data">SerializedData</param>
        /// <returns>Json string</returns>
        public static string SdToJson(SerializedData data)
        {
            return data.AsJson();
        }

        /// <summary>
        /// Converts SerializedData to Url params representation.
        /// </summary>
        /// <param name="data">SerializedData</param>
        /// <returns>Url params as string</returns>
        internal static string SdToUrlParams(SerializedData data)
        {
            //TODO typecheck
            var parameters = SimpleJsonSerializer.DeserializeWithoutRawJson<Dictionary<string, string>>(data);
            return string.Join('&',
                   parameters.Select(q => $"{HttpUtility.UrlEncode(q.Key)}={HttpUtility.UrlEncode(q.Value)}"));
        }

        /// <summary>
        /// Retrieves a value as a string from SerializedData by key.
        /// </summary>
        /// <param name="obj">The SerializedData object.</param>
        /// <param name="key">The key to look for in the serialized data.</param>
        /// <returns>The value as a string if found; otherwise, null.</returns>
        public static string? GetSdValueByKey(SerializedData obj, string key)
        {
            try
            {
                if (obj.IsJson)
                {
                    var jsonData = obj.AsJson();
                    var dictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonData);
                    if (dictionary != null && dictionary.TryGetValue(key, out var value))
                    {
                        return value?.ToString();
                    }
                }
                else
                {
                    throw new NotSupportedException("Only JSON data is currently supported.");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to retrieve value by key '{key}' from SerializedData.", ex);
            }

            return null;
        }


        /// <summary>
        /// Returns a string replacement for sensitive data.
        /// </summary>
        /// <returns>A string replacement for sensitive data.</returns>
        internal static string SanitizedValue() => "---[redacted]---";


        /// <summary>
        /// Sanitizes a SerializedData from sensitive data.
        /// </summary>
        /// <param name="sd">SerializedData to sanitize.</param>
        /// <param name="keysToSanitize">Keys to sanitize.</param>
        /// <returns>Sanitized SerializedData.</returns>
        internal static SerializedData SanitizeSerializedData(SerializedData sd, Dictionary<string, string> keysToSanitize)
        {
            try
            {
                using (var document = JsonDocument.Parse(sd.AsJson()))
                {
                    return new SerializedData(SanitizeJsonElement(document.RootElement, keysToSanitize));
                }
            }
            catch
            {
                return sd;
            }
        }

        private static object? SanitizeJsonElement(JsonElement element, Dictionary<string, string> keysToSanitize)
        {
            return element.ValueKind switch
            {
                JsonValueKind.Object => SanitizeJsonObject(element, keysToSanitize),
                JsonValueKind.Array => element.EnumerateArray().Select(item => SanitizeJsonElement(item, keysToSanitize)).ToList(),
                JsonValueKind.String => element.GetString(),
                JsonValueKind.Number => element.TryGetInt32(out int intValue) ? (object)intValue : element.GetDouble(),
                JsonValueKind.True => true,
                JsonValueKind.False => false,
                JsonValueKind.Null => null,
                _ => throw new JsonException($"Unexpected JsonValueKind: {element.ValueKind}")
            };
        }

        private static Dictionary<string, object?> SanitizeJsonObject(JsonElement element, Dictionary<string, string> keysToSanitize)
        {
            var sanitizedDictionary = new Dictionary<string, object?>();

            foreach (var property in element.EnumerateObject())
            {
                var sanitizedValue = SanitizeJsonElement(property.Value, keysToSanitize);
                sanitizedDictionary[property.Name] = keysToSanitize.ContainsKey(property.Name.ToLower()) && sanitizedValue is string
                    ? SanitizedValue()
                    : sanitizedValue;
            }

            return sanitizedDictionary;
        }

        internal static string? SanitizeFormEncodedBodyFromString(string? body, Dictionary<string, string> keysToSanitize)
        {
            if (body == null)
            {
                return null;
            }

            return string.Join("&", body.Split('&').Select(parameter => SanitizeFormEncodedParameter(parameter, keysToSanitize)));
        }

        private static string SanitizeFormEncodedParameter(string parameter, Dictionary<string, string> keysToSanitize)
        {
            var separatorIndex = parameter.IndexOf('=');
            if (separatorIndex < 0)
            {
                return parameter;
            }

            var key = parameter.Substring(0, separatorIndex);
            var value = parameter.Substring(separatorIndex + 1);
            var sanitizedValue = keysToSanitize.ContainsKey(key.ToLower()) ? SanitizedValue() : value;
            return $"{key}={sanitizedValue}";
        }
    }
}
