using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace Box.Sdk.Gen.Internal
{
    static class SerializerOptions
    {
        internal static JsonSerializerOptions getOptions()
        {
            return new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                NumberHandling = JsonNumberHandling.AllowReadingFromString,
                TypeInfoResolver = new DefaultJsonTypeInfoResolver
                {
                    Modifiers = { JsonResolverCallbacks.IncludeExplicitNulls, JsonResolverCallbacks.InternalExtraDataHandling }
                }
            };
        }
    }
}
