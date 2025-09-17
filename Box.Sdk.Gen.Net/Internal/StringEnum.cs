using System;
using System.ComponentModel;
using System.Reflection;

namespace Box.Sdk.Gen
{
    public class StringEnum<T> where T : struct, Enum
    {
        public T? Value { get; }
        public string StringValue { get; }

        public StringEnum(T? value)
        {
            if (value != null)
            {
                Value = value;
                StringValue = GetDescription((T)value);
            }
            else
            {
                Value = null;
                StringValue = string.Empty;
            }
        }

        internal StringEnum(string value)
        {
            Value = null;
            StringValue = value;
        }

        public static implicit operator StringEnum<T>(T value) => new StringEnum<T>(value);

        internal string GetDescription(T enumValue)
        {
            FieldInfo? field = enumValue.GetType().GetField(enumValue.ToString());
            DescriptionAttribute? attribute = field?.GetCustomAttribute<DescriptionAttribute>();
            return attribute == null ? enumValue.ToString() : attribute.Description;
        }
    }
}
