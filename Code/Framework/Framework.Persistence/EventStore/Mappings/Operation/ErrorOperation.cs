using Newtonsoft.Json.Linq;

namespace Framework.Persistence.EventStore.Mappings.Operation;

internal class ErrorOperation : IOperation
{
    private readonly string _errorText;
    public ErrorOperation(string errorText)
    {
        _errorText = errorText;
    }

    public ErrorOperation() : this("Unknown error in mapping")
    {
            
    }

    public JObject Apply(JObject json)
    {
        throw new EventMappingException(this._errorText);   
    }
}