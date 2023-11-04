using Framework.Core.Contracts;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text.Json;

namespace Framework.JsonSerializer.NewtonSoft;

public class NewtonSoftSerializerAdapter : IJsonSerializerAdapter
{
    private readonly JsonSerializerSettings _defaultSettings;

    public NewtonSoftSerializerAdapter(JsonSerializerSettings settings)
    {
       _defaultSettings = settings;
    }
   
    public TOutput? Deserialize<TOutput>(string input)
    {
        return string.IsNullOrWhiteSpace(input) ? default : JsonConvert.DeserializeObject<TOutput>(input,_defaultSettings);
    }

    public object? Deserialize(string input, Type type)
    {
        return string.IsNullOrWhiteSpace(input) ? default : JsonConvert.DeserializeObject(input, type, _defaultSettings);
    }

    public string? Serilize<TInput>(TInput input)
    {
        return input == null ? string.Empty : JsonConvert.SerializeObject(input, _defaultSettings);
    }
}
