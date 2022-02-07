using System;
using System.Collections.Generic;

namespace Box.V2.Test.Extensions
{
    public static class DictionaryExtensions
    {
        public static bool ContainsKeyValue<T>(this Dictionary<T, T> dictionary,
                     T expectedKey, T expectedValue) where T : IEquatable<T>
        {
            return dictionary.TryGetValue(expectedKey, out T actualValue) && EqualityComparer<T>.Default.Equals(actualValue, expectedValue);
        }
    }
}
