using Newtonsoft.Json.Linq;

namespace Framework.Persistence.EventStore.Mappings.Operation;

internal interface IOperation
{
    JObject Apply(JObject json);
}