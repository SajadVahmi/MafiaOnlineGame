namespace Framework.Core.Contracts;

public interface IJsonSerializerAdapter
{
    string? Serilize<TInput>(TInput input);

    TOutput? Deserialize<TOutput>(string input);

    object? Deserialize(string input, Type type);
}