using Newtonsoft.Json.Linq;

namespace Framework.Persistence.EventStore.Mappings.Operation;

internal class AnotherPropertyOperation : IOperation
{
    private readonly string _key;
    private readonly string _otherProperty;
    internal AnotherPropertyOperation(string key, string otherProperty)
    {
        _key = key;
        _otherProperty = otherProperty;
    }
    public JObject Apply(JObject json)
    {
        json[_key] = json[_otherProperty];
        json.Remove(_otherProperty);
        return json;
    }
}