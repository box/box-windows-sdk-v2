using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace Box.Sdk.Gen.Internal
{
    static class JsonResolverCallbacks
    {
        internal static void IncludeExplicitNulls(JsonTypeInfo jsonTypeInfo)
        {
            var privateProps = jsonTypeInfo.Type
                .GetProperties(BindingFlags.Instance | BindingFlags.NonPublic)
                .Select(p => p.GetCustomAttribute<JsonPropertyNameAttribute>()?.Name)
                .ToHashSet();

            foreach (var propInfo in jsonTypeInfo.Properties)
            {
                //remove _isFieldSet fields from json
                if (privateProps.Contains(propInfo.Name))
                {
                    propInfo.ShouldSerialize = static (obj, value) => false;
                    continue;
                }
                var isSetFieldName = $"_is{propInfo.Name}Set";
                var isSetField = jsonTypeInfo.Properties.FirstOrDefault(propInfo => propInfo.Name == isSetFieldName);

                if (isSetField != null)
                {
                    propInfo.ShouldSerialize = (obj, value) =>
                    {
                        var properties = obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.NonPublic);
                        var foundProp = properties.FirstOrDefault(p => p.GetCustomAttribute<JsonPropertyNameAttribute>()?.Name == isSetFieldName);
                        return (bool)foundProp!.GetValue(obj)!;
                    };
                }

            }
        }

        internal static void InternalExtraDataHandling(JsonTypeInfo jsonTypeInfo)
        {
            if (jsonTypeInfo.Kind == JsonTypeInfoKind.Object && jsonTypeInfo.Properties.All(prop => !prop.IsExtensionData))
            {
                PropertyInfo? extensionProp = jsonTypeInfo.Type
                    .GetProperties(BindingFlags.Instance | BindingFlags.NonPublic)
                    .FirstOrDefault(prop => prop.GetCustomAttribute<JsonExtensionDataAttribute>() != null);

                if (extensionProp != null)
                {
                    JsonPropertyInfo jsonPropertyInfo = jsonTypeInfo.CreateJsonPropertyInfo(extensionProp.PropertyType, extensionProp.Name);
                    jsonPropertyInfo.Get = extensionProp.GetValue;
                    jsonPropertyInfo.Set = extensionProp.SetValue;
                    jsonPropertyInfo.IsExtensionData = true;
                    jsonTypeInfo.Properties.Add(jsonPropertyInfo);
                }
            }
        }
    }
}
