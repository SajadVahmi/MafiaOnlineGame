using Framework.Core.Contracts;
using Newtonsoft.Json;

namespace Framework.JsonSerializer.NewtonSoft;

public class NewtonSoftSerializerAdapter(JsonSerializerSettings settings) : IJsonSerializerAdapter
{
    public TOutput? Deserialize<TOutput>(string input)
    {
        return string.IsNullOrWhiteSpace(input) ? default : JsonConvert.DeserializeObject<TOutput>(input,settings);
    }

    public object? Deserialize(string input, Type type)
    {
        return string.IsNullOrWhiteSpace(input) ? default : JsonConvert.DeserializeObject(input, type, settings);
    }

    public string? Serialize<TInput>(TInput input)
    {
        return input == null ? null : JsonConvert.SerializeObject(input, settings);
    }
}
