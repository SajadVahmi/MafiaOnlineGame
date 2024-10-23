using Newtonsoft.Json.Linq;

namespace Framework.Persistence.EventStore.Mappings.Filters;

internal class EndFilter : IFilter
{
    internal static IFilter Instance = new EndFilter();
    private EndFilter() {}
    public void SetNext(IFilter next)
    {
        throw new NotSupportedException("Can't set next on EndFilter");
    }

    public JObject Apply(JObject json)
    {
        return json;
    }
}