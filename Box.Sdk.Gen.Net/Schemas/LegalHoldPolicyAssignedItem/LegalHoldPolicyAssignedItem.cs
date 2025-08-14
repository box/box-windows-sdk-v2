using Box.Sdk.Gen;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    [JsonConverter(typeof(LegalHoldPolicyAssignedItemConverter))]
    public class LegalHoldPolicyAssignedItem {
        internal OneOf<File, Folder, WebLink> _oneOf;
        
        public File? File => _oneOf._val0;
        
        public Folder? Folder => _oneOf._val1;
        
        public WebLink? WebLink => _oneOf._val2;
        
        public LegalHoldPolicyAssignedItem(File value) {_oneOf = new OneOf<File, Folder, WebLink>(value);}
        
        public LegalHoldPolicyAssignedItem(Folder value) {_oneOf = new OneOf<File, Folder, WebLink>(value);}
        
        public LegalHoldPolicyAssignedItem(WebLink value) {_oneOf = new OneOf<File, Folder, WebLink>(value);}
        
        public static implicit operator LegalHoldPolicyAssignedItem(File value) => new LegalHoldPolicyAssignedItem(value);
        
        public static implicit operator LegalHoldPolicyAssignedItem(Folder value) => new LegalHoldPolicyAssignedItem(value);
        
        public static implicit operator LegalHoldPolicyAssignedItem(WebLink value) => new LegalHoldPolicyAssignedItem(value);
        
        class LegalHoldPolicyAssignedItemConverter : JsonConverter<LegalHoldPolicyAssignedItem> {
            public override LegalHoldPolicyAssignedItem Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
                using var document = JsonDocument.ParseValue(ref reader);
                var discriminant0Present = document.RootElement.TryGetProperty("type", out var discriminant0);
                if (discriminant0Present) {
                    switch (discriminant0.ToString()){
                        case "file":
                            return JsonSerializer.Deserialize<File>(document) ?? throw new Exception($"Could not deserialize {document} to File");
                        case "folder":
                            return JsonSerializer.Deserialize<Folder>(document) ?? throw new Exception($"Could not deserialize {document} to Folder");
                        case "web_link":
                            return JsonSerializer.Deserialize<WebLink>(document) ?? throw new Exception($"Could not deserialize {document} to WebLink");
                    }
                }
                throw new Exception($"Discriminant not found in json payload {document.RootElement} while try to converting to type {typeToConvert}");
            }

            public override void Write(Utf8JsonWriter writer, LegalHoldPolicyAssignedItem? value, JsonSerializerOptions options) {
                if (value?.File != null) {
                    JsonSerializer.Serialize(writer, value.File, options);
                    return;
                }
                if (value?.Folder != null) {
                    JsonSerializer.Serialize(writer, value.Folder, options);
                    return;
                }
                if (value?.WebLink != null) {
                    JsonSerializer.Serialize(writer, value.WebLink, options);
                    return;
                }
            }

        }

    }
}