namespace Framework.Core.Contracts;

public interface ISerializerAdapter
{
    string Serilize<TInput>(TInput input);

    TOutput Deserialize<TOutput>(string input);

    object Deserialize(string input, Type type);
}