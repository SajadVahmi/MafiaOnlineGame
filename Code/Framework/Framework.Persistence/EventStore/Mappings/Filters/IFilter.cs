using Newtonsoft.Json.Linq;

namespace Framework.Persistence.EventStore.Mappings.Filters;

public interface IFilter
{
    void SetNext(IFilter next);
    JObject Apply(JObject json);
}