using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json;

namespace Box.Sdk.Gen.Internal
{
    public static class StringUtils
    {
        public static string? ToStringRepresentation<T>(T? obj)
        {
            if (obj == null)
            {
                return null;
            }
            Type objType = obj.GetType();
            var isList = (obj is IList || obj is IEnumerable) && objType.IsGenericType;
            if (obj != null && isList)
            {
                var listOfStrings = new List<string?>();
                var asList = (IList)obj;
                foreach (var value in asList)
                {
                    if (value != null)
                    {
                        listOfStrings.Add(ToStringRepresentation(value));
                    }
                }
                var listAsString = string.Join(",", listOfStrings);
                return anyNonPrimitives(asList) ? $"[{listAsString}]" : listAsString;
            }
            else if (obj is Enum)
            {
                var objectAsString = obj.ToString();
                if (objectAsString == null)
                {
                    return null;
                }
                var fieldInfo = objType.GetField(objectAsString);

                if (fieldInfo == null)
                {
                    return null;
                }

                DescriptionAttribute[] attributes =
                    (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

                return attributes[0].Description;
            }
            else if (objType != null && objType.IsGenericType && objType.GetGenericTypeDefinition() == typeof(StringEnum<>))
            {
                var field = objType.GetProperty("StringValue");
                if (field == null)
                {
                    return null;
                }
                var propertyValue = field.GetValue(obj);
                if (propertyValue == null)
                {
                    return null;
                }
                return (string)propertyValue;
            }
            else if (obj is DateTimeOffset dateTime)
            {
                return Utils.DateTimeToString(dateTime);
            }
            else if (isNotPrimitive(obj))
            {
                return JsonSerializer.Serialize(obj);
            }
            else
            {
                return obj?.ToString();
            }
        }

        private static bool isNotPrimitive(object? obj) =>
            obj != null &&
            !obj.GetType().IsPrimitive &&
            obj.GetType() != typeof(string) &&
            !IsStringEnumType(obj.GetType());

        private static bool IsStringEnumType(Type type)
        {
            if (type.IsGenericType)
            {
                var genericTypeDefinition = type.GetGenericTypeDefinition();
                return genericTypeDefinition == typeof(StringEnum<>);
            }

            return false;
        }

        private static bool anyNonPrimitives(IList list)
        {
            foreach (var item in list)
            {
                if (isNotPrimitive(item))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
