using Newtonsoft.Json.Linq;

namespace Framework.Persistence.EventStore.Mappings.Conditions;

internal interface ICondition
{
    string PropertyName { get; }
    bool IsSatisfied(JObject json);
}