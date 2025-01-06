using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;


namespace server.Utlis
{
    public static class Utils
    {
        public static ErrorMesage ValidationError(ModelStateDictionary modelState) {
            IEnumerable<ValidationMessage> errors = modelState
                .Where(m => m.Value?.Errors.Any() ?? false)
                .Select(m => new ValidationMessage
                {
                    Field = m.Key,
                    Message = string.Join('\n', m.Value?.Errors.Select(x => x.ErrorMessage) ?? [])
                });
            return new ErrorMesage
            {
                Errors = errors
            };
        }
    }

public class JsonStringEnumMemberConverter : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert.IsEnum;
    }

    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        var converterType = typeof(JsonStringEnumMemberConverter<>).MakeGenericType(typeToConvert);
        return (JsonConverter)Activator.CreateInstance(converterType);
    }
}

public class JsonStringEnumMemberConverter<T> : JsonConverter<T> where T : struct, Enum
{
    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        foreach (var field in typeof(T).GetFields())
        {
            if (Attribute.GetCustomAttribute(field, typeof(EnumMemberAttribute)) is EnumMemberAttribute attribute && attribute.Value == value)
            {
                return (T)field.GetValue(null);
            }
        }
        throw new JsonException($"Unknown value '{value}' for enum '{typeof(T)}'");
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        var enumValue = value.ToString();
        var field = typeof(T).GetField(enumValue);
        if (field != null && Attribute.GetCustomAttribute(field, typeof(EnumMemberAttribute)) is EnumMemberAttribute attribute)
        {
            writer.WriteStringValue(attribute.Value);
        }
        else
        {
            writer.WriteStringValue(enumValue);
        }
    }
}

}