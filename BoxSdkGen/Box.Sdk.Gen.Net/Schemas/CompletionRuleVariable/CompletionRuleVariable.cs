using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class CompletionRuleVariable : ISerializable {
        /// <summary>
        /// Completion
        /// Rule object type.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<CompletionRuleVariableTypeField>))]
        public StringEnum<CompletionRuleVariableTypeField> Type { get; }

        /// <summary>
        /// Variable type
        /// for the Completion
        /// Rule object.
        /// </summary>
        [JsonPropertyName("variable_type")]
        [JsonConverter(typeof(StringEnumConverter<CompletionRuleVariableVariableTypeField>))]
        public StringEnum<CompletionRuleVariableVariableTypeField> VariableType { get; }

        /// <summary>
        /// Variable
        /// values for a completion
        /// rule.
        /// </summary>
        [JsonPropertyName("variable_value")]
        [JsonConverter(typeof(StringEnumConverter<CompletionRuleVariableVariableValueField>))]
        public StringEnum<CompletionRuleVariableVariableValueField> VariableValue { get; }

        public CompletionRuleVariable(CompletionRuleVariableVariableValueField variableValue, CompletionRuleVariableTypeField type = CompletionRuleVariableTypeField.Variable, CompletionRuleVariableVariableTypeField variableType = CompletionRuleVariableVariableTypeField.TaskCompletionRule) {
            Type = type;
            VariableType = variableType;
            VariableValue = variableValue;
        }
        
        [JsonConstructorAttribute]
        internal CompletionRuleVariable(StringEnum<CompletionRuleVariableVariableValueField> variableValue, StringEnum<CompletionRuleVariableTypeField> type, StringEnum<CompletionRuleVariableVariableTypeField> variableType) {
            Type = CompletionRuleVariableTypeField.Variable;
            VariableType = CompletionRuleVariableVariableTypeField.TaskCompletionRule;
            VariableValue = variableValue;
        }
        internal string? RawJson { get; set; } = default;

        void ISerializable.SetJson(string json) {
            RawJson = json;
        }

        string? ISerializable.GetJson() {
            return RawJson;
        }

        /// <summary>
        /// Returns raw json response returned from the API.
        /// </summary>
        public Dictionary<string, object?>? GetRawData() {
            return SimpleJsonSerializer.GetAllFields(this);
        }

    }
}