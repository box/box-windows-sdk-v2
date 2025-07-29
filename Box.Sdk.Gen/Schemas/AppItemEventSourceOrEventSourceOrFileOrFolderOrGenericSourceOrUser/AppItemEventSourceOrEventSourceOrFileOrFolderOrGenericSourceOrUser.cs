using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    [JsonConverter(typeof(AppItemEventSourceOrEventSourceOrFileOrFolderOrGenericSourceOrUserConverter))]
    public class AppItemEventSourceOrEventSourceOrFileOrFolderOrGenericSourceOrUser : OneOf<AppItemEventSource, EventSource, File, Folder, Dictionary<string, object>, User> {
        public AppItemEventSource? AppItemEventSource => _val0;
        
        public EventSource? EventSource => _val1;
        
        public File? File => _val2;
        
        public Folder? Folder => _val3;
        
        public Dictionary<string, object>? GenericSource => _val4;
        
        public User? User => _val5;
        
        public AppItemEventSourceOrEventSourceOrFileOrFolderOrGenericSourceOrUser(AppItemEventSource value) : base(value) {}
        
        public AppItemEventSourceOrEventSourceOrFileOrFolderOrGenericSourceOrUser(EventSource value) : base(value) {}
        
        public AppItemEventSourceOrEventSourceOrFileOrFolderOrGenericSourceOrUser(File value) : base(value) {}
        
        public AppItemEventSourceOrEventSourceOrFileOrFolderOrGenericSourceOrUser(Folder value) : base(value) {}
        
        public AppItemEventSourceOrEventSourceOrFileOrFolderOrGenericSourceOrUser(Dictionary<string, object> value) : base(value) {}
        
        public AppItemEventSourceOrEventSourceOrFileOrFolderOrGenericSourceOrUser(User value) : base(value) {}
        
        public static implicit operator AppItemEventSourceOrEventSourceOrFileOrFolderOrGenericSourceOrUser(AppItemEventSource value) => new AppItemEventSourceOrEventSourceOrFileOrFolderOrGenericSourceOrUser(value);
        
        public static implicit operator AppItemEventSourceOrEventSourceOrFileOrFolderOrGenericSourceOrUser(EventSource value) => new AppItemEventSourceOrEventSourceOrFileOrFolderOrGenericSourceOrUser(value);
        
        public static implicit operator AppItemEventSourceOrEventSourceOrFileOrFolderOrGenericSourceOrUser(File value) => new AppItemEventSourceOrEventSourceOrFileOrFolderOrGenericSourceOrUser(value);
        
        public static implicit operator AppItemEventSourceOrEventSourceOrFileOrFolderOrGenericSourceOrUser(Folder value) => new AppItemEventSourceOrEventSourceOrFileOrFolderOrGenericSourceOrUser(value);
        
        public static implicit operator AppItemEventSourceOrEventSourceOrFileOrFolderOrGenericSourceOrUser(Dictionary<string, object> value) => new AppItemEventSourceOrEventSourceOrFileOrFolderOrGenericSourceOrUser(value);
        
        public static implicit operator AppItemEventSourceOrEventSourceOrFileOrFolderOrGenericSourceOrUser(User value) => new AppItemEventSourceOrEventSourceOrFileOrFolderOrGenericSourceOrUser(value);
        
        class AppItemEventSourceOrEventSourceOrFileOrFolderOrGenericSourceOrUserConverter : JsonConverter<AppItemEventSourceOrEventSourceOrFileOrFolderOrGenericSourceOrUser> {
            public override AppItemEventSourceOrEventSourceOrFileOrFolderOrGenericSourceOrUser Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
                using var document = JsonDocument.ParseValue(ref reader);
                var discriminant0Present = document.RootElement.TryGetProperty("type", out var discriminant0);
                if (discriminant0Present) {
                    switch (discriminant0.ToString()){
                        case "app_item":
                            return JsonSerializer.Deserialize<AppItemEventSource>(document) ?? throw new Exception($"Could not deserialize {document} to AppItemEventSource");
                        case "file":
                            return JsonSerializer.Deserialize<File>(document) ?? throw new Exception($"Could not deserialize {document} to File");
                        case "folder":
                            return JsonSerializer.Deserialize<Folder>(document) ?? throw new Exception($"Could not deserialize {document} to Folder");
                        case "user":
                            return JsonSerializer.Deserialize<User>(document) ?? throw new Exception($"Could not deserialize {document} to User");
                    }
                }
                var discriminant1Present = document.RootElement.TryGetProperty("item_type", out var discriminant1);
                if (discriminant1Present) {
                    switch (discriminant1.ToString()){
                        case "file":
                            return JsonSerializer.Deserialize<EventSource>(document) ?? throw new Exception($"Could not deserialize {document} to EventSource");
                        case "folder":
                            return JsonSerializer.Deserialize<EventSource>(document) ?? throw new Exception($"Could not deserialize {document} to EventSource");
                    }
                }
                try {
                    var result = JsonSerializer.Deserialize<Dictionary<string, object>>(document, new JsonSerializerOptions() { UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow });
                    if (result != null) {
                        return result;
                    }
                } catch {
                    
                }
                throw new Exception($"Discriminant not found in json payload {document.RootElement} while try to converting to type {typeToConvert}");
            }

            public override void Write(Utf8JsonWriter writer, AppItemEventSourceOrEventSourceOrFileOrFolderOrGenericSourceOrUser? value, JsonSerializerOptions options) {
                if (value?.AppItemEventSource != null) {
                    JsonSerializer.Serialize(writer, value.AppItemEventSource, options);
                    return;
                }
                if (value?.EventSource != null) {
                    JsonSerializer.Serialize(writer, value.EventSource, options);
                    return;
                }
                if (value?.File != null) {
                    JsonSerializer.Serialize(writer, value.File, options);
                    return;
                }
                if (value?.Folder != null) {
                    JsonSerializer.Serialize(writer, value.Folder, options);
                    return;
                }
                if (value?.GenericSource != null) {
                    JsonSerializer.Serialize(writer, value.GenericSource, options);
                    return;
                }
                if (value?.User != null) {
                    JsonSerializer.Serialize(writer, value.User, options);
                    return;
                }
            }

        }

    }
}