using Newtonsoft.Json.Linq;

namespace Framework.Persistence.EventStore.Mappings.Conditions;

internal class AbsentCondition : ICondition
{
    public string PropertyName { get; private set; }

    public AbsentCondition(string propertyName)
    {
        this.PropertyName = propertyName;
    }


    public bool IsSatisfied(JObject json)
    {
        return !json.ContainsKey(PropertyName);
    }
}