namespace Framework.Core.Contracts;

public interface IJsonSerializerAdapter
{
    string? Serialize<TInput>(TInput input);

    TOutput? Deserialize<TOutput>(string input);

    object? Deserialize(string input, Type type);
}