using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Box.V2.Test.Extensions
{
    public static class DictionaryExtensions
    {
        public static bool ContainsKeyValue<T>(this Dictionary<T, T> dictionary,
                     T expectedKey, T expectedValue) where T : IEquatable<T>
        {
            return dictionary.TryGetValue(expectedKey, out T actualValue) && EqualityComparer<T>.Default.Equals(actualValue, expectedValue);
        }

        public static bool ContainsKeyValue(this string json,
             string expectedKey, string expectedValue)
        {
            var currentToken = JsonConvert.DeserializeObject<JToken>(json);
            foreach (var key in expectedKey.Split('.'))
            {
                var isNumber = int.TryParse(key, out var numberKey);
                currentToken = isNumber ? currentToken.Value<JToken>(numberKey) : currentToken.Value<JToken>(key);
            }
            return expectedValue == currentToken.ToString();
        }
    }
}
