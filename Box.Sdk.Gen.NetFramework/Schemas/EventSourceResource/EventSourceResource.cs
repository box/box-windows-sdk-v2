using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    [JsonConverter(typeof(EventSourceResourceConverter))]
    public class EventSourceResource {
        internal OneOf<User, EventSource, File, Folder, Dictionary<string, object>, AppItemEventSource> _oneOf;
        
        public User User => _oneOf._val0;
        
        public EventSource EventSource => _oneOf._val1;
        
        public File File => _oneOf._val2;
        
        public Folder Folder => _oneOf._val3;
        
        public Dictionary<string, object> GenericSource => _oneOf._val4;
        
        public AppItemEventSource AppItemEventSource => _oneOf._val5;
        
        public EventSourceResource(User value) {_oneOf = new OneOf<User, EventSource, File, Folder, Dictionary<string, object>, AppItemEventSource>(value);}
        
        public EventSourceResource(EventSource value) {_oneOf = new OneOf<User, EventSource, File, Folder, Dictionary<string, object>, AppItemEventSource>(value);}
        
        public EventSourceResource(File value) {_oneOf = new OneOf<User, EventSource, File, Folder, Dictionary<string, object>, AppItemEventSource>(value);}
        
        public EventSourceResource(Folder value) {_oneOf = new OneOf<User, EventSource, File, Folder, Dictionary<string, object>, AppItemEventSource>(value);}
        
        public EventSourceResource(Dictionary<string, object> value) {_oneOf = new OneOf<User, EventSource, File, Folder, Dictionary<string, object>, AppItemEventSource>(value);}
        
        public EventSourceResource(AppItemEventSource value) {_oneOf = new OneOf<User, EventSource, File, Folder, Dictionary<string, object>, AppItemEventSource>(value);}
        
        public static implicit operator EventSourceResource(User value) => new EventSourceResource(value);
        
        public static implicit operator EventSourceResource(EventSource value) => new EventSourceResource(value);
        
        public static implicit operator EventSourceResource(File value) => new EventSourceResource(value);
        
        public static implicit operator EventSourceResource(Folder value) => new EventSourceResource(value);
        
        public static implicit operator EventSourceResource(Dictionary<string, object> value) => new EventSourceResource(value);
        
        public static implicit operator EventSourceResource(AppItemEventSource value) => new EventSourceResource(value);
        
        class EventSourceResourceConverter : JsonConverter<EventSourceResource> {
            public override EventSourceResource Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
                using (var document = JsonDocument.ParseValue(ref reader)){
                    var discriminant0Present = document.RootElement.TryGetProperty("type", out var discriminant0);
                    if (discriminant0Present) {
                        switch (discriminant0.ToString()){
                            case "user":
                                return JsonSerializer.Deserialize<User>(document) ?? throw new Exception($"Could not deserialize {document} to User");
                            case "file":
                                return JsonSerializer.Deserialize<File>(document) ?? throw new Exception($"Could not deserialize {document} to File");
                            case "folder":
                                return JsonSerializer.Deserialize<Folder>(document) ?? throw new Exception($"Could not deserialize {document} to Folder");
                            case "app_item":
                                return JsonSerializer.Deserialize<AppItemEventSource>(document) ?? throw new Exception($"Could not deserialize {document} to AppItemEventSource");
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
            }

            public override void Write(Utf8JsonWriter writer, EventSourceResource value, JsonSerializerOptions options) {
                if (value?.User != null) {
                    JsonSerializer.Serialize(writer, value.User, options);
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
                if (value?.AppItemEventSource != null) {
                    JsonSerializer.Serialize(writer, value.AppItemEventSource, options);
                    return;
                }
            }

        }

    }
}