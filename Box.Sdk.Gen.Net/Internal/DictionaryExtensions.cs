using System;
using System.Collections.Generic;
using System.Linq;

namespace Box.Sdk.Gen.Internal
{
   /// <summary>
   /// Class for various dictionary extension utils functions.
   /// </summary>
   public static class DictionaryUtils
   {
      /// <summary>
      /// Creates a dictionary by merging two dictionaries into one.
      /// If the same key exists in both dictionaries, dict2 takes precedence.
      /// </summary>
      /// <param name="dict1">First dictionary to merge.</param>
      /// <param name="dict2">Second dictionary to merge.</param>
      /// <returns>A new dictionary with the combined keys and values of `dict1` dictionary and `dict2.</returns>
      public static Dictionary<TKey, TValue?> MergeDictionaries<TKey, TValue>(Dictionary<TKey, TValue?> dict1, Dictionary<TKey, TValue?>? dict2) where TKey : class
      {
         var merged = dict2?.Union(dict1.Where(k => !dict2.ContainsKey(k.Key))).ToDictionary(k => k.Key, v => v.Value) ?? dict1;
         return merged ?? new Dictionary<TKey, TValue?>();
      }
   }
}
